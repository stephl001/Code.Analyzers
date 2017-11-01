#---------------------------------------------------------------------------------------
#
# This script is used to run the same build process on dev machines and on official 
# build machines. By using this script on your dev machine, you are therefore using 
# the same steps as on the build machine.
#
# Note that the script Build.ps1 makes it easier to call Build.psake.ps1. You should
# be using Build.ps1 instead.
#
# GUIDELINES:
# -----------
#
# In order to avoid surprises, the script follows these guidelines:
#
#   - all the tasks executed on a dev machine will be executed the exact same way
#     on the build machine.
#   - some tasks may be done only on the build machine (ex: SonarQube analysis) or 
#     only on dev machines (ex: deploy environment)
#   - compiling from the build script should do the same thing as compiling from 
#     within VisualStudio.
#  
#---------------------------------------------------------------------------------------


# Fail on first error.
$ErrorActionPreference = 'Stop'


#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#
#                                 psake properties
#
# The following properties can be overwritten from the command line by using the 
# -properties option of Invoke-psake.
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------

properties {
    $Configuration            = 'Debug'   # Can be: Debug, Release
    $IsRunningOnBuildMachine  = $false
    $MsbuildVerbosity         = 'normal'  # Can be: q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic]
    $NugetVerbosity           = 'quiet'   # Can be: normal, quiet, detailed
    $NugetFeed                = $null     # Will be set only when running on build machine.
    $NugetUser                = $null     # Will be set only when running on build machine.
    $NugetPassword            = $null     # Will be set only when running on build machine.
}



#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#
#                                High level targets
#
# The following targets describe the overall flow of the script. See the sub targets
# for more details.
#
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------

Task default      -depends All # This task needs to be provided because -Docs does not return the 'default' task. I therefore introduce a dummy task.

Task All          -depends Clean,
                           Compile,
                           Test

Task Clean        -depends CleanPackages,
                           CleanSolutions,
                           CleanArtifacts `
             -precondition {$IsRunningOnBuildMachine -eq $false}   # No need to clean on build machine since we always start from a brand new workspace.

Task Compile      -depends RestorePackages,
                           CompileSolutions,
                           GeneratePackages,
                           PublishPackages



#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#
#                               Global initializations
#
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------

# This function changes what psake prints when a task starts.
FormatTaskName {
   param($taskName)
   "[$(Get-Date -f 'yyyy-MM-dd HH:mm:ss')] $taskName"
}

# Since we don't stop psake when test fails, we need a flag that will track how many 
# tests fail.
$psake.number_of_test_run_errors = 0

$WorkspaceRoot = Split-Path $psake.build_script_dir -Parent

# Download nuget packages that are required very soon in the build process 
# (i.e. before RestorePackages is called)
$packages = Copy-Item "$WorkspaceRoot\build\packages.bootstrap.config" -Destination (Join-Path $env:TEMP 'packages.config') -Force -PassThru
Exec {& "$WorkspaceRoot\lib\nuget\nuget.exe" restore $packages.FullName -PackagesDirectory "$WorkspaceRoot\packages\NuGet" -ConfigFile "$WorkspaceRoot\nuget.config" -Verbosity quiet}
Get-ChildItem "$WorkspaceRoot\packages\NuGet" -Recurse -Include Orckestra.PsUtil.psd1 |
    ForEach {Import-Module $_.FullName -Global -Force -Verbose:$false} 

$ArtifactsStagingDirectory                = "$WorkspaceRoot\artifacts"
$TroubleshootingArtifactsStagingDirectory = "$ArtifactsStagingDirectory\Troubleshooting"
$CentralLogsFolder                        = "$TroubleshootingArtifactsStagingDirectory\Logs"
$TestResultsCentralDirectory              = "$TroubleshootingArtifactsStagingDirectory\Tests"
$NugetArtifactsDirectory                  = "$ArtifactsStagingDirectory\Nuget"

function Create-ArtifactsFolder
{
    New-FolderIfNotExists $ArtifactsStagingDirectory
    New-FolderIfNotExists $TroubleshootingArtifactsStagingDirectory
    New-FolderIfNotExists $CentralLogsFolder
    New-FolderIfNotExists $TestResultsCentralDirectory
    New-FolderIfNotExists $NugetArtifactsDirectory
}

Create-ArtifactsFolder


#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#
#                                     Psake tasks
#
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------

Task CleanPackages {
    
    Remove-FolderIfExists -Folder (Join-Path $WorkspaceRoot 'packages')
}

Task CleanSolutions {
    
    Get-AllSolutions |
        Invoke-MsbuildClean -Configuration $Configuration
}

Task CleanArtifacts {

    Remove-FolderIfExists -Folder $ArtifactsStagingDirectory

    Create-ArtifactsFolder
}

Task RestorePackages {

    $toRestore = ("$WorkspaceRoot\Build\packages.bootstrap.config",
                  "$WorkspaceRoot\Build\packages.config",
                  (Get-AllSolutions))

    $toRestore | Invoke-NugetRestore -NugetVerbosity $NugetVerbosity
}

Task CompileSolutions {
    Get-AllSolutions | 
        Invoke-Msbuild -Configuration $Configuration -LogsDirectory $CentralLogsFolder -MsbuildVerbosity $MsbuildVerbosity -IsRunningOnBuildMachine:$IsRunningOnBuildMachine
}

Task GeneratePackages {
    Find-CsProjsThatGenerateNugetPackage | 
        Find-NuspecInOutputFolder -Configuration $Configuration |
        Invoke-NugetPackage -Version (Get-GitVersion).NuGetVersion -OutputDirectory $NugetArtifactsDirectory -NugetVerbosity $NugetVerbosity
}

Task PublishPackages {

    if ($NugetUser -and $NugetPassword -and $NugetFeed)
    {
        # Only publish packages in build jobs that the nuget 
        # publishing information. 
        #
        # Note that the build definition used for the pull request
        # does not publish packages.

        Get-ChildItem $NugetArtifactsDirectory\*.nupkg | 
            Publish-NugetPackage -Feed $NugetFeed -User $NugetUser -Password $NugetPassword -NugetVerbosity $NugetVerbosity
    }

    Start-VstsArtifactUpload -ArtifactName 'NugetPackages' -Directory $NugetArtifactsDirectory -IsRunningOnBuildMachine:$IsRunningOnBuildMachine
}

Task Test {
    Find-TestContainers -Configuration $Configuration | 
        Invoke-XUnit -RunName 'Unit' -Parallelism none -IsRunningOnBuildMachine:$IsRunningOnBuildMachine -Coverage:$true
}

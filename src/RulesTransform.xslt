<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <head>
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="stylesheet" href="http://yui.yahooapis.com/pure/0.6.0/pure-min.css" />
        <style>
          h1 {
          font-family: "Avant Garde", Avantgarde, "Century Gothic", CenturyGothic, "AppleGothic", sans-serif;
          font-size: 62px;
          text-align: center;
          text-transform: uppercase;
          text-rendering: optimizeLegibility;          
          }
        </style>
      </head>
      <body>
        <xsl:apply-templates/>
      </body>
    </html>
  </xsl:template>
  <xsl:template match="rule-set">
    <h1>
      <xsl:value-of select="@name" />
      <xsl:text> rules</xsl:text>
    </h1>
    <table class="pure-table">
      <thead>
        <tr>
          <th>Rule Name</th>
          <th>Description</th>
          <th>Severity</th>
        </tr>
      </thead>

      <tbody>
        <xsl:for-each select="rule">
          <xsl:variable name="css-class">
            <xsl:choose>
              <xsl:when test="position() mod 2 = 0">pure-table-odd</xsl:when>
              <xsl:otherwise></xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <tr class="{$css-class}">
            <td>
              <xsl:value-of select="@id" />
            </td>
            <td>
              <xsl:value-of select="description" />
            </td>
            <td>
              <xsl:value-of select="severity" />
            </td>
          </tr>
        </xsl:for-each>
      </tbody>
    </table>
  </xsl:template>
</xsl:stylesheet>

using System;
using System.Activities;
using System.Collections.Generic;

namespace Orckestra.Overture.DurableTask
{
    /// <summary>
    /// Abstract class to be used as base class for custom activities.
    /// This can be used for basic tasks that do not require full access to the workflow’s runtime features (such as Bookmarks).
    /// </summary>
    public abstract class TaskActivity : AsyncCodeActivity
    {
        private int _backingField;
        private readonly double _readOnlyBackingField;
        private const string MyConstant = "Test";

        protected string Property { get; }

        public string WritableProperty { get;  set; }

        public string WritableOnlyProperty { set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivity"/> class.
        /// </summary>
        protected TaskActivity()
        {
            _readOnlyBackingField = 983.3d;
        }

        /// <summary>
        /// Gets or sets the URL of the blob containing the result.
        /// </summary>
        public OutArgument<string> ResultBlobUrl { get; set; }

        /// <summary>
        /// Gets or sets the age of the captain.
        /// </summary>
        public InArgument<int> Age { get; set; }

        /// <summary>
        /// Using the specified execution context, callback method, and user state, enqueues an asynchronous activity in a run-time workflow.
        /// </summary>
        /// <param name="context">Information that defines the execution environment for the <see cref="T:System.Activities.AsyncCodeActivity" />.</param>
        /// <param name="callback">The method to be called after the asynchronous activity and completion notification have occurred.</param>
        /// <param name="state">An object that saves variable information for an instance of an asynchronous activity.</param>
        /// <returns>
        /// The object that saves variable information for an instance of an asynchronous activity.
        /// </returns>
        protected sealed override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Using the specified execution context, enqueues an asynchronous activity in a run-time workflow.
        /// </summary>
        /// <param name="context">Information that defines the execution environment for the <see cref="T:System.Activities.AsyncCodeActivity" />.</param>
        /// <param name="asyncResult">The asynchronous result.</param>
        protected override sealed void EndExecute(AsyncCodeActivityContext context, IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When implemented in a derived class and using the specified execution environment information, 
        /// notifies the workflow runtime that the asynchronous activity operation has reached an early completion. Serves as a virtual method.
        /// </summary>
        /// <param name="context">Information that defines the execution environment for the <see cref="T:System.Activities.AsyncCodeActivity" />.</param>
        protected override sealed void Cancel(AsyncCodeActivityContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is used to perform operations before the invocation of the <see cref="ExecuteAsync"/> method.
        /// It can be used to access functionalities of the the Windows Workflow Foundation context.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="userState">State of the user.</param>
        protected virtual void PreExecute(AsyncCodeActivityContext context, IDictionary<string, object> userState) { }

        /// <summary>
        /// This method is used to perform operations after the invocation of the <see cref="ExecuteAsync"/> method.
        /// It can be used to access functionalities of the the Windows Workflow Foundation context.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="userState">State of the user.</param>
        protected virtual void PostExecute(AsyncCodeActivityContext context, IDictionary<string, object> userState) { }
    }
}
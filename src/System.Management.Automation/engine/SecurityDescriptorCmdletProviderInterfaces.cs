/********************************************************************++
Copyright (c) Microsoft Corporation.  All rights reserved.
--********************************************************************/

using System.Collections.ObjectModel;
using System.Security.AccessControl;
using Dbg=System.Management.Automation;

namespace System.Management.Automation
{
    /// <summary>
    /// Provides the *-SecurityDescriptor noun for the cmdlet providers.  
    /// </summary>
    ///
    public sealed class SecurityDescriptorCmdletProviderIntrinsics
    {
        #region Constructors

        /// <summary>
        /// Hide the default constructor since we always require an instance of SessionState.
        /// </summary>
        private SecurityDescriptorCmdletProviderIntrinsics()
        {
            Dbg.Diagnostics.Assert(
                false,
                "This constructor should never be called. Only the constructor that takes an instance of SessionState should be called.");
        } // CmdletProviderIntrinsics private


        /// <summary>
        /// Initializes a new instance of the SecurityDescriptorCmdletProviderIntrinsics
        /// class, using the Cmdlet parameter to obtain access to the SessionState APIs.
        /// </summary>
        ///
        /// <param name="cmdlet">
        /// An instance of the cmdlet.
        /// </param>
        internal SecurityDescriptorCmdletProviderIntrinsics(Cmdlet cmdlet)
        {
            if (cmdlet == null)
            {
                throw PSTraceSource.NewArgumentNullException("cmdlet");
            }

            this.cmdlet = cmdlet;
            this.sessionState = cmdlet.Context.EngineSessionState;
        } // CmdletProviderIntrinsics internal

        /// <summary>
        /// Initializes a new instance of the SecurityDescriptorCmdletProviderIntrinsics
        /// class, using the sessionState parameter to obtain access to the SessionState APIs.
        /// </summary>
        ///
        /// <param name="sessionState">
        /// An instance of the real session state class.
        /// </param>
        internal SecurityDescriptorCmdletProviderIntrinsics(SessionStateInternal sessionState)
        {
            if (sessionState == null)
            {
                throw PSTraceSource.NewArgumentNullException("sessionState");
            }

            this.sessionState = sessionState;
        } // CmdletProviderIntrinsics internal

        #endregion Constructors

        #region Public methods

        #region GetSecurityDescriptor

        /// <summary>
        /// Gets the SecurityDescriptor at the specified path, including only the specified
        /// AccessControlSections.
        /// </summary>
        ///
        /// <param name="path">
        /// The path of the item to retrieve. It may be a drive or provider-qualified path and may include.
        /// glob characters.
        /// </param>
        /// <param name="includeSections">
        /// The sections of the security descriptor to include.
        /// </param>
        /// 
        /// <returns>
        /// The SecurityDescriptor(s) at the specified path.
        /// </returns>
        public Collection<PSObject> Get(string path, AccessControlSections includeSections)
        {
            Dbg.Diagnostics.Assert(
                sessionState != null,
                "The only constructor for this class should always set the sessionState field");

            // Parameter validation is done in the session state object
            return sessionState.GetSecurityDescriptor(path, includeSections);
        } // GetSecurityDescriptor

        /// <summary>
        /// Gets the SecurityDescriptor at the specified path, including only the specified
        /// AccessControlSections, using the provided Context.
        /// </summary>
        ///
        /// <param name="path">
        /// The path of the item to retrieve. It may be a drive or provider-qualified path and may include
        /// glob characters.
        /// </param>
        /// <param name="includeSections">
        /// The sections of the security descriptor to include.
        /// </param>
        /// <param name="context">
        /// The context under which the command is running.
        /// </param>
        ///
        /// <returns>
        /// Nothing. The object(s) at the specified path are written to the context.
        /// </returns>
        internal void Get(string path,
                        AccessControlSections includeSections,
                        CmdletProviderContext context)
        {
            Dbg.Diagnostics.Assert(
                sessionState != null,
                "The only constructor for this class should always set the sessionState field");

            // Parameter validation is done in the session state object
            sessionState.GetSecurityDescriptor(path, includeSections, context);
        } // GetSecurityDescriptor

        #endregion GetSecurityDescriptor

        #region SetSecurityDescriptor

        /// <summary>
        /// Sets the provided SecurityDescriptor at the specified path.
        /// </summary>
        ///
        /// <param name="path">
        /// The path of the item to set. It may be a drive or provider-qualified path and may include
        /// glob characters.
        /// </param>
        /// <param name="sd">
        /// The new security descriptor to set.
        /// </param>
        ///
        /// <returns>
        /// The SecurityDescriptor(s) set at the specified path.
        /// </returns>
        public Collection<PSObject> Set(string path, ObjectSecurity sd)
        {
            Dbg.Diagnostics.Assert(
                sessionState != null,
                "The only constructor for this class should always set the sessionState field");

            // Parameter validation is done in the session state object
            Collection<PSObject> result = sessionState.SetSecurityDescriptor(path, sd);

            return result;
        } // SetSecurityDescriptor

        /// <summary>
        /// Sets the SecurityDescriptor at the specified path, using the provided Context.
        /// </summary>
        ///
        /// <param name="path">
        /// The path of the item to set. It may be a drive or provider-qualified path and may include
        /// glob characters.
        /// </param>
        /// <param name="sd">
        /// The new security descriptor to set.
        /// </param>
        /// <param name="context">
        /// The context under which the command is running.
        /// </param>
        ///
        /// <returns>
        /// Nothing. The object(s) set at the specified path are written to the context.
        /// </returns>
        internal void Set(string path, ObjectSecurity sd, CmdletProviderContext context)
        {
            Dbg.Diagnostics.Assert(
                sessionState != null,
                "The only constructor for this class should always set the sessionState field");

            // Parameter validation is done in the session state object

            sessionState.SetSecurityDescriptor(path, sd, context);
        } // SetSecurityDescriptor

        #endregion SetSecurityDescriptor

        #region NewSecurityDescriptor

        /// <summary>
        /// Creates a new SecurityDescriptor from the item at the specified path, including only the specified
        /// AccessControlSections.
        /// </summary>
        ///
        /// <param name="path">
        /// The path of the item to retrieve. It may be a drive or provider-qualified path and may include
        /// glob characters.
        /// </param>
        /// <param name="includeSections">
        /// The sections of the security descriptor to include.
        /// </param>
        /// 
        /// <returns>
        /// The SecurityDescriptor(s) at the specified path.
        /// </returns>
        public ObjectSecurity NewFromPath(string path, AccessControlSections includeSections)
        {
            Dbg.Diagnostics.Assert(
                sessionState != null,
                "The only constructor for this class should always set the sessionState field");

            // Parameter validation is done in the session state object
            return sessionState.NewSecurityDescriptorFromPath(path, includeSections);
        } // NewSecurityDescriptor


        /// <summary>
        /// Creates a new SecurityDescriptor from the specified provider and of the given type,
        /// including only the specified AccessControlSections.
        /// </summary>
        ///
        /// <param name="providerId">
        /// The name of the provider.
        /// </param>
        /// <param name="type">
        /// The type of the item which corresponds to the security
        /// descriptor that we want to create.
        /// </param>
        /// <param name="includeSections">
        /// The sections of the security descriptor to include.
        /// </param>
        /// 
        /// <returns>
        /// A new SecurityDescriptor of the specified type.
        /// </returns>
        ///
        public ObjectSecurity NewOfType(string providerId, string type, AccessControlSections includeSections)
        {
            Dbg.Diagnostics.Assert(
                sessionState != null,
                "The only constructor for this class should always set the sessionState field");

            // Parameter validation is done in the session state object

            return sessionState.NewSecurityDescriptorOfType(providerId,
                                                            type,
                                                            includeSections);
        } // NewSecurityDescriptor

        #endregion NewSecurityDescriptor

        #endregion Public methods

        #region private data

        private Cmdlet cmdlet;
        private SessionStateInternal sessionState;

        #endregion private data
    } // SecurityDescriptorCmdletProviderIntrinsics
}

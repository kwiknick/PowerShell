// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Management.Automation
{
    /// <summary>
    /// This is a wrapper for exception class
    /// <see cref="System.NotSupportedException"/>
    /// which provides additional information via
    /// <see cref="System.Management.Automation.IContainsErrorRecord"/>.
    /// </summary>
    /// <remarks>
    /// Instances of this exception class are usually generated by the
    /// Monad Engine.  It is unusual for code outside the Monad Engine
    /// to create an instance of this class.
    /// </remarks>
    [Serializable]
    public class PSNotSupportedException
            : NotSupportedException, IContainsErrorRecord
    {
        #region ctor
        /// <summary>
        /// Initializes a new instance of the PSNotSupportedException class.
        /// </summary>
        /// <returns> constructed object </returns>
        public PSNotSupportedException()
            : base()
        {
        }

        #region Serialization
        /// <summary>
        /// Initializes a new instance of the PSNotSupportedException class
        /// using data serialized via
        /// <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info"> serialization information </param>
        /// <param name="context"> streaming context </param>
        /// <returns> constructed object </returns>
        protected PSNotSupportedException(SerializationInfo info,
                                            StreamingContext context)
                : base(info, context)
        {
            _errorId = info.GetString("ErrorId");
        }

        /// <summary>
        /// Serializer for <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info"> serialization information </param>
        /// <param name="context"> streaming context </param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new PSArgumentNullException("info");
            }

            base.GetObjectData(info, context);
            info.AddValue("ErrorId", _errorId);
        }
        #endregion Serialization

        /// <summary>
        /// Initializes a new instance of the PSNotSupportedException class.
        /// </summary>
        /// <param name="message">  </param>
        /// <returns> constructed object </returns>
        public PSNotSupportedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PSNotSupportedException class.
        /// </summary>
        /// <param name="message">  </param>
        /// <param name="innerException">  </param>
        /// <returns> constructed object </returns>
        public PSNotSupportedException(string message,
                        Exception innerException)
                : base(message, innerException)
        {
        }
        #endregion ctor

        /// <summary>
        /// Additional information about the error
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Note that ErrorRecord.Exception is
        /// <see cref="System.Management.Automation.ParentContainsErrorRecordException"/>.
        /// </remarks>
        public ErrorRecord ErrorRecord
        {
            get
            {
                if (null == _errorRecord)
                {
                    _errorRecord = new ErrorRecord(
                        new ParentContainsErrorRecordException(this),
                        _errorId,
                        ErrorCategory.NotImplemented,
                        null);
                }
                return _errorRecord;
            }
        }
        private ErrorRecord _errorRecord;
        private string _errorId = "NotSupported";
    } // PSNotSupportedException
} // System.Management.Automation


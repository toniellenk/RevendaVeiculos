using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace RevendaVeiculos.SendGrid.Abstractions
{
    [Serializable]
    public class EmailException : Exception
    {
        public EmailException()
        {
        }

        public EmailException(string message)
            : base(message)
        {
        }

        public EmailException(string message, Exception inner)
            : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected EmailException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

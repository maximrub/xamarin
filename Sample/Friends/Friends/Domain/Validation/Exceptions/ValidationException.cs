using System;
using System.Collections.Generic;

namespace Friends.Domain.Validation.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> Messages { get; private set; }

        public ValidationException(string[] i_Messages) : this(i_Messages, null)
        {
        }

        public ValidationException(string[] i_Messages, Exception i_InnerException) : base(string.Join(Environment.NewLine, i_Messages), i_InnerException)
        {
            Messages = i_Messages;
        }
    }
}
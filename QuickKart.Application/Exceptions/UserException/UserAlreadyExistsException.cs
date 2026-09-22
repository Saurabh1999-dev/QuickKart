using System;
using System.Collections.Generic;
using System.Text;

namespace QuickKart.Application.Exceptions.UserException
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}

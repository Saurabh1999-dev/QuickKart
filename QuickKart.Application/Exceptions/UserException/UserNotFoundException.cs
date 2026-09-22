using System;
using System.Collections.Generic;
using System.Text;

namespace QuickKart.Application.Exceptions.UserException
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message)
            : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBooth.BL.Exceptions
{
    public class UiException : Exception
    {
        public string MessageForUser { get; set; }
    }
}

using System;

namespace PhotoBooth.BL.Exceptions
{
    public class UiException : Exception
    {
        public string MessageForUser { get; set; }
    }
}
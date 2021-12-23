using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Notifier
{
    public class Notification
    {
        public string Error { get; private set; }

        public Notification(string error)
        {
            Error = error;
        }
    }
}

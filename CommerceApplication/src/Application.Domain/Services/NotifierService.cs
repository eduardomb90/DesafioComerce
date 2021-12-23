using Application.Domain.Interfaces;
using Application.Domain.Notifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Domain.Services
{
    public class NotifierService : INotifierService
    {
        private List<Notification> list = new List<Notification>();


        public NotifierService()
        {
        }

        public void AddError(string error)
        {
            list.Add(new Notification(error));
        }

        public IEnumerable<Notification> GetErrors()
        {
            return list;
        }

        public bool HasError()
        {
            return list.Any();
        }
    }
}

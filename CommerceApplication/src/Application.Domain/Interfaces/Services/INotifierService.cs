using Application.Domain.Notifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Interfaces.Services
{
    public interface INotifierService
    {
        void AddError(string error);
        IEnumerable<Notification> GetErrors();
        bool HasError();
    }
}

using MediatR;
using Sammo.Sso.Domain.Events.Values;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sammo.Sso.Domain.EventHandlers
{
    public class ValueEventHandler : INotificationHandler<ValueChangedEvent>
    {
        public Task Handle(ValueChangedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("value is changed" + notification.Id);
            return Task.CompletedTask;
        }
    }
}

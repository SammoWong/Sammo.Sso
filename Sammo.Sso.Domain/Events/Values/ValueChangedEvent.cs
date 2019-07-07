using Sammo.Sso.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sammo.Sso.Domain.Events.Values
{
    public class ValueChangedEvent : Event
    {
        public ValueChangedEvent(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

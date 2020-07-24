using System;
using Prism.Events;

namespace Samba.Presentation.Services.Common
{
    public class GenericEvent<TValue> : PubSubEvent<EventParameters<TValue>> { }
    public class GenericIdEvent : PubSubEvent<EventParameters<int>> { }

    public class EventParameters<TValue>
    {
        public string Topic { get; set; }
        public Action ExpectedAction { get; set; }
        public TValue Value { get; set; }
    }
}

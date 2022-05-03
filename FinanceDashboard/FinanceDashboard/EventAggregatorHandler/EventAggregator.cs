using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Chess.EventAggregatorHandler
{
    public class EventAggregator : IEventAggregator
    {
        private readonly List<Delegate> messageHandlers = new ();

        private readonly SynchronizationContext messageSynchronizationContext;

        public EventAggregator()
        {
            messageSynchronizationContext = SynchronizationContext.Current;
        }

        public void SendMessage<T>(T message)
        {
            if (message == null)
            {
                return;
            }

            if (messageSynchronizationContext != null)
            {
                messageSynchronizationContext.Send(m => Dispatch<T>((T)m), message);
            }
            else
            {
                Dispatch(message);
            }
        }

        public void PostMessage<T>(T message)
        {
            if (message == null)
            {
                return;
            }

            if (messageSynchronizationContext != null)
            {
                messageSynchronizationContext.Post(m => Dispatch<T>((T)m), message);
            }
            else
            {
                Dispatch(message);
            }
        }

        public Action<T> RegisterHandler<T>(Action<T> eventHandler)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }

            messageHandlers.Add(eventHandler);
            return eventHandler;
        }

        public void UnregisterHandler<T>(Action<T> eventHandler)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }

            messageHandlers.Remove(eventHandler);
        }

        public void UnregisterAll()
        {
            messageHandlers.Clear();
        }

        private void Dispatch<T>(T message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            foreach (var h in messageHandlers.OfType<Action<T>>().ToList())
            {
                h(message);
            }
        }
    }
}

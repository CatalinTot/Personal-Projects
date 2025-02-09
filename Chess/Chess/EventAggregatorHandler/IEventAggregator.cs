﻿using System;

namespace Chess.EventAggregatorHandler
{
    public interface IEventAggregator
    {
        void SendMessage<T>(T message);

        void PostMessage<T>(T message);

        Action<T> RegisterHandler<T>(Action<T> eventHandler);

        void UnregisterHandler<T>(Action<T> eventHandler);
    }
}

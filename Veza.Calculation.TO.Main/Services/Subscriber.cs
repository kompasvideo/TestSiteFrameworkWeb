using System;

namespace Veza.HeatExchanger.Services
{
    /// <summary>
    /// Вспомогательный класс для передачи сообщений между страницами
    /// используется MessageBus
    /// </summary>
    sealed class MessageSubscriber : IDisposable
    {
        private readonly Action<MessageSubscriber> action;

        public Type ReceiverType { get; }
        public Type MessageType { get; }

        public MessageSubscriber(Type receiverType, Type messageType, Action<MessageSubscriber> action)
        {
            ReceiverType = receiverType;
            MessageType = messageType;
            this.action = action;
        }

        public void Dispose()
        {
            action?.Invoke(this);
        }
    }
}

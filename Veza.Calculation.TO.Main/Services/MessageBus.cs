using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Veza.HeatExchanger.Messages;

namespace Veza.HeatExchanger.Services
{
    sealed public class MessageBus
    {
        #region Внутренние поля и переменные
        private readonly ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>> consumers;
        #endregion

        #region Конструктор
        public MessageBus()
        {
            consumers = new ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>>();
        }
        #endregion

        #region Методы
        public async Task SendTo<TReceiver>(IMessage message)
        {
            var messageType = message.GetType();
            var receiverType = typeof(TReceiver);

            var tasks = consumers
                .Where(s => s.Key.MessageType == messageType && s.Key.ReceiverType == receiverType)
                .Select(s => s.Value(message));

            await Task.WhenAll(tasks);
        }

        public IDisposable Receive<TMessage>(object receiver, Func<TMessage, Task> handler) where TMessage : IMessage
        {
            var sub = new MessageSubscriber(receiver.GetType(), typeof(TMessage), s => consumers.TryRemove(s, out var _));

            consumers.TryAdd(sub, (@event) => handler((TMessage)@event));

            return sub;
        }
        #endregion
    }
}

using SamMRoberts.CardGame.Components;

namespace SamMRoberts.CardGame.Management
{
    public class Mediator : IMediator, IQueueable
    {
        private IQueue _queue;
        private readonly Dictionary<string, Components.IComponent> _components;

        public Mediator(IQueue _queue)
        {
            this._queue = _queue;
            _components = [];
        }

        public void Register(Components.IComponent component)
        {
            //_components.Add(component);
            _components.Add(component.GetType().Name, component);
        }

        public void Notify(object sender, ICommand command)
        {
            switch (sender)
            {
                case IHandler<string> handler:
                    _queue.Enqueue(command);
                    return;
                case IInteractiveConsole console:
                    _queue.Enqueue(command);
                    return;
            }
            _queue.Enqueue(command);
        }

        public void Notify(object sender, string ev)
        {
            switch (sender)
            {
                case IInteractiveConsole console:
                    _components.Values.OfType<IHandler<string>>().ToList().ForEach(c => c.Handle(ev));
                    break;
            }
        }

        public void Send(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}
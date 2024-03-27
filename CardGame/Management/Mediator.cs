using SamMRoberts.CardGame.Components;

namespace SamMRoberts.CardGame.Management
{
    public class Mediator : IMediator, IQueueable
    {
        private IQueue _queue;
        private readonly Dictionary<string, Components.IComponent> _components = [];

        //public Mediator(IQueue _queue)
        //{
        //    this._queue = _queue;
        //    _components = [];
        //}

        public void Register(Components.IComponent component)
        {
            //_components.Add(component);
            _components.Add(component.GetType().Name, component);
            component.SetMediator(this);
        }

        public void Send0(Components.IComponent component, ICommand command)
        {
            //_queue.Enqueue(command);
            if (component != this)
                component.Receive(command);
        }

        public void Send0(Components.IComponent component, string ev)
        {
            if (component != this)
                _components.Values.OfType<IHandler<string>>().ToList().ForEach(c => c.Handle(ev));
        }


        public void Send(Components.IComponent component, ICommand command)
        {
            switch (component)
            {
                case IHandler<string> handler:
                    //_components.Values.OfType<IQueue>().ToList().ForEach(c => c.Enqueue(command));
                    _components.Values.OfType<IQueue>().ToList().ForEach(c => c.Receive(command));
                    return;
                case IInteractiveConsole console:
                    return;
            }
            _queue.Enqueue(command);
        }

        public void Send(Components.IComponent component, string ev)
        {
            switch (component)
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
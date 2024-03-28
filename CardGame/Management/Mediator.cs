using SamMRoberts.CardGame.Components;

namespace SamMRoberts.CardGame.Management
{
    public class Mediator : IMediator
    {
        private IQueue _queue;
        private Dictionary<string, Components.IComponent> _components = [];
        private IList<Games.IPlayer> _players = [];

        //public Mediator(IQueue _queue)
        //{
        //    this._queue = _queue;
        //    _components = [];
        //}

        public void Register(Components.IComponent component)
        {
            if (component is Games.IPlayer player)
            {
                _players.Add(player);
            }
            else
            {
                _components.Add(component.GetType().Name, component);
            }
            component.SetMediator(this);
        }

        public void Send(Components.IComponent component, ICommand command)
        {
            switch (component)
            {
                case IHandler<string> handler:
                    _components.Values.OfType<IQueue>().ToList().ForEach(c => c.Receive(command));
                    return;
                case IInteractiveConsole console:
                    return;
            }
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
    }
}
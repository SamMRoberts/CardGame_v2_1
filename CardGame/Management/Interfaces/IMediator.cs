namespace SamMRoberts.CardGame.Management
{
    public interface IMediator
    {
        public void Send(Components.IComponent component, ICommand command);
        void Send(Components.IComponent component, string ev);
        void Register(Components.IComponent component);
    }
}
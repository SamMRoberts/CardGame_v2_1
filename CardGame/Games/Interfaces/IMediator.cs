namespace SamMRoberts.CardGame.Management
{
    public interface IMediator
    {
        public void Notify(object sender, ICommand command);
        void Notify(object sender, string ev);
        void Register(Components.IComponent component);
    }
}
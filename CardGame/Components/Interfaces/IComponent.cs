namespace SamMRoberts.CardGame.Components
{
    public interface IComponent
    {
        void SetMediator(Management.IMediator mediator);
        void Receive(object sender, string ev);
    }
}
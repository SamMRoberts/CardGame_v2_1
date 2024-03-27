namespace SamMRoberts.CardGame.Components
{
    public interface IComponent
    {
        void SetMediator(Management.IMediator mediator);
        void Receive(Management.ICommand command);
        void Send(Management.ICommand command);
    }
}
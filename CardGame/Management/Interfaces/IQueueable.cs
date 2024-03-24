namespace SamMRoberts.CardGame.Management
{
    public interface IQueueable
    {
        void Send(ICommand command);
    }
}
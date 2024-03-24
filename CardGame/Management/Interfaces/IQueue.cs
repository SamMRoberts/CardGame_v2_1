namespace SamMRoberts.CardGame.Management
{
    public interface IQueue
    {
        void Enqueue(ICommand command);
    }
}
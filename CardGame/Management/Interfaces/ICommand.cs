namespace SamMRoberts.CardGame.Management
{
    public interface ICommand
    {
        Action Action { get; }
        void Execute();
    }
}
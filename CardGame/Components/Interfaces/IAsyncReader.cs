namespace SamMRoberts.CardGame.Components
{
    public interface IAsyncReader<T>
    {
        Task<T> ReadLineAsync();
    }
}
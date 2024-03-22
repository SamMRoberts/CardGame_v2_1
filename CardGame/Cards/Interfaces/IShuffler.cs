namespace SamMRoberts.CardGame.Cards
{
    public interface IShuffler
    {
        void Shuffle<T>(IList<T> list);
    }
}
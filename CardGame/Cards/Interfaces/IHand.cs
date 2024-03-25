namespace SamMRoberts.CardGame.Cards
{
    public enum HandSort
    {
        None,
        Ascending,
        Descending,
        BySuit,
        ByFace,
    }

    public interface IHand<T> : IEnumerable<T>
    {
        void Clear();
        bool Contains(T item);
        void AddLast(T value);
        #if _sortable
        void AddFirst(T value);
        void AddRandom(T value);
        T GetFirst();
        T GetLast();
        T GetRandom();
        T Get(T value);
        void Sort(HandSort primarySort, HandSort secondarySort = HandSort.None);
        #endif
    }
}
namespace SamMRoberts.CardGame.Cards
{
    public interface IDeck<T> : IEnumerable<T>
    {
        void AddTop(T value);
        void AddBottom(T value);
        void AddRandom(T value);
        T GetTop();
        T GetBottum();
        T GetRandom();
        void Clear();
        int Count { get; }
        public T this[int i] { get; set; }
    }
}
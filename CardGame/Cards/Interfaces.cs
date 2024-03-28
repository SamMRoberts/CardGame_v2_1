namespace SamMRoberts.CardGame.Cards;

public enum HandSort
{
    None,
    Ascending,
    Descending,
    BySuit,
    ByFace,
}

public interface IHand<T> : IEnumerable<T>, IHandAdder<T>, IHandGetter<T>
{
    void Clear();
    bool Contains(T item);
    #if _sortable
    void Sort(HandSort primarySort, HandSort secondarySort = HandSort.None);
    #endif
}

public interface IHandGetter<T> : IEnumerable<T>
{
    T Get(T value);
    #if _sortable
    T GetFirst();
    T GetLast();
    T GetRandom();
    #endif
}

public interface IHandAdder<T> : IEnumerable<T>
{
    void AddLast(T value);
    #if _sortable
    void AddFirst(T value);
    void AddRandom(T value);
    #endif
}

public interface IShuffler
{
    void GetAndShuffle();
    IDeck<Card> Shuffle(IDeck<Card> deck);
}

public interface IDeck<T> : IEnumerable<T>, IDeckAdder<T>, IDeckGetter<T>
{
    void Clear();
    int Count { get; }
    public T this[int i] { get; set; }
}

public interface IDeckAdder<T> : IEnumerable<T>
{
    void AddTop(T value);
    void AddBottom(T value);
    void AddRandom(T value);
}

public interface IDeckGetter<T> : IEnumerable<T>
{
    T GetTop();
    T GetBottom();
    T GetRandom();
}

public interface IDeckBuilder<TFace, TSuit>
    where TFace : Enum
    where TSuit : Enum
{
    IDeck<Card> BuildDeck<TFaceDelegate>(TFaceDelegate faceDelegate)
        where TFaceDelegate : Delegate;
}

public interface ICardHolder
{
    Card Card { get; set; }
    CardHolder.Visibility Visible { get; set; }
}
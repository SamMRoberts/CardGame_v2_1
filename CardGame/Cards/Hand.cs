using System.Collections;

namespace SamMRoberts.CardGame.Cards;

public class Hand<T> : IHand<T>
    where T : ICardHolder
{
    public Hand()
    {
        _sortable = false;
        cards = new T[52];
    }

    public Hand(int maxSize = 52)
    {
        _sortable = false;
        cards = new T[maxSize];
    }

    public Hand(bool sortable = false, int maxSize = 52)
    {
        _sortable = sortable;
        cards = new T[maxSize];
    }
    private readonly bool _sortable;
    private T[] cards;
    private int nextIndex = 0;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => nextIndex;

    public T this[int i]
    {
        get { return cards[i]; }
        set { cards[i] = value; }
    }

    #if _sortable
    public void AddFirst(T value)
    {
        if (nextIndex >= cards.Length)
            throw new IndexOutOfRangeException($"The collection can hold only {cards.Length} elements.");
        for (int i = nextIndex; i > 0; i--)
        {
            cards[i] = cards[i - 1];
        }
        cards[0] = value;
        nextIndex++;
    }
    #endif

    public void AddLast(T value)
    {
        if (nextIndex >= cards.Length)
            throw new IndexOutOfRangeException($"The collection can hold only {cards.Length} elements.");
        cards[nextIndex++] = value;
    }

    public void Clear()
    {
        Array.Fill(cards, default(T));
        nextIndex = 0;
    }

    public bool Contains(T item)
    {
        foreach (T card in cards)
        {
            if (card?.Equals(item) == true)
                return true;
        }
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T card in cards)
        {
            if (card.Visible == CardHolder.Visibility.FaceDown || card.Visible == CardHolder.Visibility.FaceUp)
                yield return card;
        }
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i]?.Equals(item) == true)
                return i;
        }
        return -1;
    }

    public T Get(T item)
    {
        var card = default(T)!;
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i]?.Equals(item) == true)
            {
                card = Get(i);
                continue;
            }
        }
        return card;
    }

    public void RemoveAt(int index)
    {
        for (int i = index; i < nextIndex - 1; i++)
        {
            cards[i] = cards[i + 1];
        }
        nextIndex--;
    }

    #if _sortable
    public void AddRandom(T value)
    {
        var random = new Random((int)DateTime.Now.Ticks);
        Insert(random.Next(0, nextIndex), value);
    }

    public T GetFirst()
    {
        return Get(nextIndex - 1);
    }

    public T GetLast()
    {
        return Get(0);
    }

    public T GetRandom()
    {
        var random = new Random((int)DateTime.Now.Ticks);
        return Get(random.Next(0, nextIndex));
    }

    public void Sort(HandSort primarySort, HandSort secondarySort = HandSort.None)
    {
        throw new NotImplementedException();
    }
    #endif

    private T Get(int index)
    {
        var temp = cards[index];
        for (int i = index; i < nextIndex; i++)
        {
            cards[i] = cards[i + 1];
        }
        cards[nextIndex] = default(T)!;
        nextIndex--;
        return temp;
    }

    private void Insert(int index, T item)
    {
        if (nextIndex >= cards.Length)
            throw new IndexOutOfRangeException($"The collection can hold only {cards.Length} elements.");
        for (int i = nextIndex; i > index; i--)
        {
            cards[i] = cards[i - 1];
        }
        cards[index] = item;
        nextIndex++;
    }
}
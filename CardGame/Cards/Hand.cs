using System.Collections;

namespace SamMRoberts.CardGame.Cards
{
    public class Hand<T>(int maxSize = 52) : IHand<T>
    {
        private T[] cards = new T[maxSize];
        private int nextIndex = 0;

        public T this[int i]
        {
            get { return cards[i]; }
            set { cards[i] = value; }
        }

        public int Count => nextIndex;

        public void AddFirst(T value)
        {
            if (nextIndex >= cards.Length)
                throw new IndexOutOfRangeException($"The collection can hold only {cards.Length} elements.");
            cards[nextIndex++] = value;
        }

        public void AddLast(T value)
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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
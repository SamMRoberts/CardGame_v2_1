using System.Collections;

namespace SamMRoberts.CardGame.Cards
{
    public class Deck<T>(int deckSize = 52) : IDeck<T>, IEnumerable<T>
    {
        private readonly T[] cards = new T[deckSize];
        private int nextIndex = 0;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => nextIndex;

        public T this[int i]
        {
            get { return cards[i]; }
            set { cards[i] = value; }
        }

        public void AddTop(T value)
        {
            if (nextIndex >= cards.Length)
                throw new IndexOutOfRangeException($"The collection can hold only {cards.Length} elements.");
            cards[nextIndex++] = value;
        }

        public void AddBottom(T value)
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
            Array.Fill(cards, default!);
            nextIndex = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (cards == null)
                throw new NullReferenceException("The collection is empty.");
            foreach (T card in cards)
            {
                yield return card;
            }
        }

        public void AddRandom(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (cards == null)
                throw new NullReferenceException("The collection is empty.");
            var random = new Random((int)DateTime.Now.Ticks);
            Insert(random.Next(0, nextIndex), value);
        }

        public T GetTop()
        {
            if (cards == null)
                throw new NullReferenceException("The collection is empty.");
            return Get(nextIndex - 1);
        }

        public T GetBottum()
        {
            if (cards == null)
                throw new NullReferenceException("The collection is empty.");
            return Get(0);
        }

        public T GetRandom()
        {
            if (cards == null)
                throw new NullReferenceException("The collection is empty.");
            var random = new Random((int)DateTime.Now.Ticks);
            return Get(random.Next(0, nextIndex));
        }

        private T Get(int index)
        {
            if (cards == null)
                throw new NullReferenceException("The collection is empty.");
            var temp = cards[index];
            for (int i = index; i < nextIndex; i++)
            {
                cards[i] = cards[i + 1];
            }
            cards[nextIndex] = default!;
            nextIndex--;
            return temp;
        }

        private void Insert(int index, T item)
        {
            if (cards == null)
                throw new NullReferenceException("The collection is empty.");
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
}
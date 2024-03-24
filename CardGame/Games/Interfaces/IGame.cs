namespace SamMRoberts.CardGame.Games
{
    public interface IGame
    {
        public Cards.IDeck<Cards.Card> Deck { get; }
        void Start();
        void Restart();
        Cards.IDeck<Cards.Card> PickupDeck();
        void PlaceDeck(Cards.IDeck<Cards.Card> deck);
    }
}
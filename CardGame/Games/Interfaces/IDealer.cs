using SamMRoberts.CardGame.Cards;

namespace SamMRoberts.CardGame.Games
{
    public interface IDealer : IPlayer
    {
        void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, int count);    // Deal a specific number of cards to a player
        void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, Cards.CardHolder.Visibility visibility);           // Deal a single card to a player
        void Deal(Cards.IDeck<Cards.Card> deck, int count);             // Deal a specific number of cards to all players
        IDeck<Cards.Card> GetDeck();
        void PutDeck(Cards.IDeck<Cards.Card> deck);
        IDeck<Card> Shuffle(IDeck<Card> deck);
        void GetAndShuffle();
    }
}
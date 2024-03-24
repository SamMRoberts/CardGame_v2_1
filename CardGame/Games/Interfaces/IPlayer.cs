namespace SamMRoberts.CardGame.Games
{
    public interface IPlayer
    {
        IGame Game { get; }
        string Name { get; set; }
        int Score { get; set; }
        Cards.Hand<Cards.Card> Hand { get; set; }
    }
}
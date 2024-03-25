namespace SamMRoberts.CardGame.Games
{
    public interface IPlayer
    {
        IGame Game { get; }
        string Name { get; set; }
        int Score { get; set; }
        Cards.Hand<Cards.CardHolder> Hand { get; set; }
    }
}
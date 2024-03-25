namespace SamMRoberts.CardGame.Cards
{
    public interface ICardHolder
    {
        Card Card { get; set; }
        CardHolder.Visibility Visible { get; set; }
    }
}
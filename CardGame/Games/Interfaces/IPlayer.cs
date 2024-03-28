namespace SamMRoberts.CardGame.Games
{
    public interface IPlayer
    {
        IGame Game { get; }
        string Name { get; set; }
        Cards.Hand<Cards.CardHolder> Hand { get; set; }
        void DisplayHand()
        {
            System.Console.WriteLine($"{this.Name} hand: {Cards.Extensions.HandExtensions.ToString(Hand)}");
        }
    }
}
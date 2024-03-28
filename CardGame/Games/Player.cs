namespace SamMRoberts.CardGame.Games
{
    public class Player : IPlayer
    {
        public IGame Game { get; }
        public string Name { get; set; }
        public Cards.Hand<Cards.CardHolder> Hand { get; set; }

        public Player(string name, IGame game)
        {
            Name = name;
            Game = game;
            Hand = new();
        }

        public Player(string name, IGame game, int handSize)
        {
            Name = name;
            Game = game;
            Hand = new(handSize);;
        }
    }
}
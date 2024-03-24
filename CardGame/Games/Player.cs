namespace SamMRoberts.CardGame.Games
{
    public class Player : IPlayer
    {
        public IGame Game { get; }
        public string Name { get; set; }
        public int Score { get; set; }
        public Cards.Hand<Cards.Card> Hand { get; set; }

        public Player(string name, IGame game)
        {
            Name = name;
            Game = game;
            Score = 0;
            Hand = new();
        }
    }
}
namespace SamMRoberts.CardGame.Management
{
    public class Manager : IManager
    {
        private static Manager? _instance;

        protected Manager()
        {
            // Do nothing
        }

        static Manager IManager.Instance()
        {
            _instance ??= new Manager();
            return _instance;
        }
    }
}
using System.Windows.Input;

namespace SamMRoberts.CardGame.Management
{
    public class Command : ICommand
    {
        private readonly Action task;
        public Command(Action task)
        {
            this.task = task;
        }

        public async void Execute()
        {
            await Task.Run(task);
        }

        public override string ToString()
        {
            return task.Method.Name;
        }
    }
}
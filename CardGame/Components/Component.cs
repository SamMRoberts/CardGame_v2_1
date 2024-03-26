using SamMRoberts.CardGame.Management;

namespace SamMRoberts.CardGame.Components
{
    public class Component : IComponent
    {
        protected IMediator _mediator;

        public Component(IMediator mediator = null)
        {
            this._mediator = mediator;
        }

        public void Receive(object sender, string ev)
        {
            throw new NotImplementedException();
        }

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }
}
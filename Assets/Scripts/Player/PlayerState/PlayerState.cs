namespace Player
{
    public abstract class PlayerState
    {
        public ChefBehaviour Chef
        {
            get;
            set;
        }
        public abstract PlayerState HandleInput();
    }
}

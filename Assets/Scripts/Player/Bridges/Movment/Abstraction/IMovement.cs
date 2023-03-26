namespace Abstraction
{
    namespace Player
    {
        public interface IMovement
        {
            void MoveForward();
            void MoveBackward();
            void MoveLeft();
            void MoveRight();
            void Jump();
        }
    }
}

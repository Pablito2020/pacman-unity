using board;

namespace game.food
{
    public interface IFoodSubscriber
    {
        void EatFoodOn(Position position);
    }
}
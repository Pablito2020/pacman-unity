using System;
using board;
using game.food;

namespace ui.food
{
    public class FoodRemover : IFoodSubscriber
    {
        Action<Position> Action;
        public FoodRemover(Action<Position> action)
        {
            this.Action = action;
        }

        public void EatFoodOn(Position position)
        {
            this.Action(position);
        }
    }
}
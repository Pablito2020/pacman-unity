using System;
using board;
using game.food;

namespace ui.food
{
    public class FoodRemover : IFoodSubscriber
    {
        private readonly Action<Position, Cell> _setCell;

        public FoodRemover(Action<Position, Cell> setCell)
        {
            this._setCell = setCell;
        }

        public void EatFoodOn(Position position)
        {
            this._setCell(position, Cell.PATH);
        }
    }
}
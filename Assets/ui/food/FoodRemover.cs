using board;
using game.food;

namespace ui.food
{
    public class FoodRemover : IFoodSubscriber
    {
        private readonly BoardDrawer _boardDrawer;

        public FoodRemover(BoardDrawer boardDrawer)
        {
            _boardDrawer = boardDrawer;
        }

        public void EatFoodOn(Position position)
        {
            _boardDrawer.Set(position, Cell.PATH);
        }
    }
}
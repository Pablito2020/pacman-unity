using System.Linq;
using board;

namespace game.initializers
{
    public class GameBoardCreator
    {
        public static Board FillWithFood(Board board, Position initialPosition)
        {
            var resultBoard = board.DeepCopy();
            for (var row = 0; row < board.GetRows(); row++)
            for (var column = 0; column < board.GetColumns(); column++)
            {
                var currentPosition = new Position(row, column);
                if (board.Get(currentPosition).IsWalkable() && !currentPosition.Equals(initialPosition))
                    resultBoard.Set(currentPosition, Cell.FOOD);
            }

            return resultBoard;
        }

        public static Board FillWithBigFoods(Board board, Position initialPosition)
        {
            var resultBoard = board.DeepCopy();
            for (var row = 0; row < board.GetRows(); row++)
            for (var column = 0; column < board.GetColumns(); column++)
            {
                var currentPosition = new Position(row, column);
                if (board.Get(currentPosition).IsWalkable() && !currentPosition.Equals(initialPosition) &&
                    board.GetNeighbours(currentPosition)
                        .Select(board.Get)
                        .Count(cell => cell is Cell.FOOD or Cell.PATH) == 1)
                    resultBoard.Set(currentPosition, Cell.BIG_FOOD);
            }

            return resultBoard;
        }
    }
}
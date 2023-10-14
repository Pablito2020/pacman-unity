using System;
using System.Collections.Generic;
using board;

namespace game.initializers
{
    public class PacmanRandomInitializer : IPacmanInitializer
    {
        public Position GetInitialPosition(Board board)
        {
            return GetRandomValidPosition(board);
        }

        private static Position GetRandomValidPosition(Board board)
        {
            return GetRandomValidPosition(board, new HashSet<Position>());
        }

        private static Position GetRandomValidPosition(Board board, ISet<Position> visited)
        {
            while (true)
            {
                var random = new Random();
                var position = new Position(random.Next(board.GetRows()), random.Next(board.GetColumns()));
                if (board.Get(position).IsWalkable()) return position;
                visited.Add(position);
            }
        }


        public Direction GetInitialDirection(GameState state)
        {
            return GetRandomValidDirection(state);
        }

        private Direction GetRandomValidDirection(GameState state)
        {
            return GetRandomValidDirection(state, new HashSet<Direction>());
        }

        private Direction GetRandomValidDirection(GameState state, ISet<Direction> visited)
        {
            while (true)
            {
                var random = new Random();
                var values = Enum.GetValues(typeof(Direction));
                var direction = (Direction)values.GetValue(random.Next(values.Length));
                if (state.CanApply(direction)) return direction;
                visited.Add(direction);
            }
        }
    }
}
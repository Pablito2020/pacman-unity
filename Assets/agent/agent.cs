using System;
using System.Collections.Generic;
using System.Linq;
using board;

namespace agent
{
    public class Agent
    {
        private readonly int maxSteps;
        private readonly Maze maze;
        private readonly float walkedThreshold;

        public Agent(Maze maze, float walkedThreshold, int maxSteps)
        {
            this.maze = maze;
            this.walkedThreshold = walkedThreshold;
            this.maxSteps = maxSteps;
        }

        public HashSet<Position> WalkFrom(Position position, HashSet<Position> walkedPositions)
        {
            var positionsVisited = new SortedSet<Position>(walkedPositions);
            WalkFrom(position, 0, positionsVisited);
            return new HashSet<Position>(positionsVisited);
        }

        private void WalkFrom(Position position, int numberOfSteps, SortedSet<Position> walkedPositions)
        {
            if (numberOfSteps == maxSteps)
                return;

            if (!maze.WalkedIsBelowThreshold(walkedThreshold))
                return;

            var nextStep = StepFromPosition(position, walkedPositions);

            if (nextStep == null)
                return;

            MarkAsWalked(nextStep, walkedPositions);
            WalkFrom(nextStep, numberOfSteps + 1, walkedPositions);
        }

        private void MarkAsWalked(Position step, SortedSet<Position> walkedPositions)
        {
            walkedPositions.Add(step);
            maze.SetAsWalked(step);
        }

        private Position? StepFromPosition(Position position, SortedSet<Position> visited)
        {
            var possiblePositions = GetPossibleNextPositionsFrom(position, visited);

            if (possiblePositions.Count == 0)
                return null;

            var random = new Random();
            var index = random.Next(0, possiblePositions.Count);

            return possiblePositions.ElementAt(index);
        }

        private HashSet<Position> GetPossibleNextPositionsFrom(Position position, SortedSet<Position> positionsVisited)
        {
            var directions = maze.GetDirectionsFrom(position);
            return directions
                .Select(dir => position.ApplyDirection(dir))
                .Where(pos => !positionsVisited.Contains(pos) && !maze.PositionIsPath(pos))
                .ToHashSet();
        }
    }
}
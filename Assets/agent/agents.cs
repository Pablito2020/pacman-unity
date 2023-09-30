using System;
using System.Collections.Generic;
using System.Linq;
using board;

namespace agent
{
    public class Agents
    {
        private readonly int maxSteps;
        private readonly int numberAgents;
        private readonly float walkThreshold;

        public Agents(int numberAgents, int maxSteps, float walkThreshold)
        {
            this.numberAgents = numberAgents;
            this.maxSteps = maxSteps;
            this.walkThreshold = walkThreshold;
        }

        public void Walk(Maze maze, HashSet<Position> startPositions)
        {
            var poolInitialPositions = new PositionPool(startPositions);
            var positionsVisited = new SortedSet<Position>(startPositions);

            for (var i = 0; i < numberAgents; i++)
            {
                var initialPosition = poolInitialPositions.Pull() ??
                                      positionsVisited.ElementAt(new Random().Next(positionsVisited.Count));
                var agent = new Agent(maze, walkThreshold, maxSteps);
                positionsVisited.UnionWith(agent.WalkFrom(initialPosition, new HashSet<Position>(positionsVisited)));
            }
        }
    }
}
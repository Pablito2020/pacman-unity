using System;
using System.Collections.Generic;
using System.Linq;
using board;

namespace agent
{
    public class PositionPool
    {
        private readonly SortedSet<Position> pool;

        public PositionPool(HashSet<Position> pool)
        {
            this.pool = new SortedSet<Position>(pool);
        }

        public Position? Pull()
        {
            if (pool.Count == 0) return null;

            // TODO: Change for a static method for random
            var random = new Random();
            var index = random.Next(pool.Count);
            var position = pool.ElementAt(index);
            pool.Remove(position);

            return position;
        }
    }
}
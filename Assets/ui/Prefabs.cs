using System;
using UnityEngine;

namespace ui
{
    /**
     * This class is a container for the prefabs used in the game.
     */
    public class Prefabs
    {
        public readonly GameObject Corridor;
        public readonly GameObject Wall;
        public readonly Func<GameObject, GameObject> Instantiate;
        public readonly Action<GameObject> Destroy;
        
        public Prefabs(GameObject corridor, GameObject wall, Func<GameObject, GameObject> instantiate, Action<GameObject> destroy)
        {
            this.Corridor = corridor;
            this.Wall = wall;
            this.Instantiate = instantiate;
            this.Destroy = destroy;
        }
    }
}
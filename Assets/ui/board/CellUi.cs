#nullable enable
using System;
using UnityEngine;

namespace ui
{
    public class CellUi
    {
        private GameObject? fruit;
        private readonly GameObject map;

        public CellUi(GameObject map, GameObject? fruit = null)
        {
            this.map = map;
            this.fruit = fruit;
        }

        public void EatFruit()
        {
            if (fruit == null) return;
            fruit = null;
        }

        public void Apply(Action<GameObject> operation)
        {
            operation(map);
            if (fruit != null)
                operation(fruit);
        }
        
        public void Destroy(Action<GameObject> destroy)
        {
            Apply(destroy);
        }
    }
}
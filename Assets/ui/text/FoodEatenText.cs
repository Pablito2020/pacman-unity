using UnityEngine;
using UnityEngine.EventSystems;

namespace ui.text
{
    public class FoodEatenText : UIBehaviour
    {
        private int _points;
        private int columns;

        private readonly string FoodElementsEaten = "Food elements eaten: ";
        private int rows;
        private int x;
        private int y;
        private Rect r = new(0, 0, 100, 100);

        private void OnEnable()
        {
            FruitEat.OnFruitEaten += () => { _points += 1; };
            GameUI.OnGameFinishedEvent += () => { _points = 0; };
            GameUI.OnMazeCreatedEvent += (rows, columns) =>
            {
                this.rows = rows;
                this.columns = columns;
                x = columns + -columns / 2 + 1;
                y = rows + -rows / 2 + 1;
                r = new Rect(x, y, rows, columns);
            };
        }
        
        public void OnGUI()
        {
            var style = new GUIStyle
            {
                fontSize = 20
            };
            GUI.Label(r, FoodElementsEaten + _points, style);
        }
    }
}
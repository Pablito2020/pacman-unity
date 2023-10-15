using UnityEngine;
using UnityEngine.EventSystems;

namespace ui.text
{
    public class BigFoodEatenText : MonoBehaviour
    {
        private const string BigFoodElementsEaten = "Big Food elements eaten: ";
        private int _points;
        private int columns;
        private int rows;
        private int x;
        private int y;

        private void OnEnable()
        {
            BigFruitEat.OnBigFruitEaten += () => { _points += 1; };
            GameUI.OnGameFinishedEvent += () => { _points = 0; };
            GameUI.OnMazeCreatedEvent += (rows, columns) =>
            {
                this.rows = rows;
                this.columns = columns;
                x = -columns / 2 + 1;
                y = -rows / 2 + 1;
            };
        }

        public void OnGUI()
        {
            var style = new GUIStyle
            {
                fontSize = 20
            };
            GUI.Label(new Rect(x, y, rows, columns), BigFoodElementsEaten + _points, style);
        }
    }
}
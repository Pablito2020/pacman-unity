namespace board
{
    public enum Cell
    {
        WALL,
        PATH,
        FOOD,
        BIG_FOOD
    }
    
    public static class CellExtensions
    {
        public static bool IsWalkable(this Cell cell)
        {
            return cell != Cell.WALL;
        }

    }
    
}
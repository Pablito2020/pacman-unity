using board;

namespace game.initializers
{
    public interface IPacmanInitializer
    {
        Position GetInitialPosition(Board board);
        Direction GetInitialDirection(GameState state);
    }

}
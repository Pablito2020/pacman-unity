using agent;
using board;
using ui;
using UnityEngine;

public class MazeUI : MonoBehaviour
{
    private const int MAX_AGENTS = 4;
    private const int MAX_STEPS_AGENT = 300;
    private const int ROWS = 100;
    private const int COLUMNS = 100;
    private const int INITIAL_DONUT_WIDTH = 7;
    private const int INITIAL_DONUT_HEIGHT = 7;
    private const float WALK_THRESHOLD = 0.5f;
    private static readonly Donut DEFAULT_DONUT_SIZE = new(INITIAL_DONUT_WIDTH, INITIAL_DONUT_HEIGHT);
    [SerializeField] public GameObject corridorSquare;
    [SerializeField] public GameObject wallSquare;
    private MazeDrawer drawer;

    private void Start()
    {
        drawer = new MazeDrawer(InstantiateObject, DestroyObject, corridorSquare, wallSquare);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) PrintMaze();
    }

    private void PrintMaze()
    {
        drawer.Destroy();
        var maze = GetRandomMaze();
        drawer.Draw(maze, gameObject);
    }

    private static Maze GetRandomMaze()
    {
        var maze = new Maze(ROWS, COLUMNS);
        var initialPositions = Cycle.Apply(DEFAULT_DONUT_SIZE, maze);
        var agents = new Agents(MAX_AGENTS, MAX_STEPS_AGENT, WALK_THRESHOLD);
        agents.Walk(maze, initialPositions);
        return maze;
    }


    private static GameObject InstantiateObject(GameObject prefab)
    {
        return Instantiate(prefab);
    }


    private static void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
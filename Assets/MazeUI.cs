using agent;
using board;
using JetBrains.Annotations;
using ui;
using UnityEngine;

public class MazeUI : MonoBehaviour
{
    [SerializeField] private int MAX_AGENTS = 4;
    [SerializeField] public int MAX_STEPS_AGENT = 300;
    [SerializeField] public int ROWS = 10;
    [SerializeField] public int COLUMNS = 10;
    [SerializeField] public int INITIAL_DONUT_WIDTH = 3;
    [SerializeField] public int INITIAL_DONUT_HEIGHT = 3;
    [SerializeField] public float WALK_THRESHOLD = 0.5f;

    [SerializeField] public GameObject corridorSquare;
    [SerializeField] public GameObject wallSquare;

    [CanBeNull] private MazeDrawer _drawer;
    private Prefabs _prefabs;

    private void Start()
    {
        _prefabs = new Prefabs(corridorSquare, wallSquare, InstantiateObject, DestroyObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) PrintMaze();
    }

    private void PrintMaze()
    {
        _drawer?.Destroy();
        var maze = GetRandomMaze();
        _drawer = new MazeDrawer(_prefabs, maze);
        _drawer.Draw(gameObject);
    }

    private Maze GetRandomMaze()
    {
        var maze = new Maze(ROWS, COLUMNS);
        var initialDonut = new Donut(INITIAL_DONUT_WIDTH, INITIAL_DONUT_HEIGHT);
        var initialPositions = Cycle.Apply(initialDonut, maze);
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
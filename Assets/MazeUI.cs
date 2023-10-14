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
    [SerializeField] public GameObject foodSquare;
    [SerializeField] public GameObject pacman;

    [CanBeNull] private GameDrawer _drawer;
    private Prefabs _prefabs;

    private void Start()
    {
        _prefabs = new Prefabs(corridorSquare, wallSquare, foodSquare, pacman, InstantiateObject, DestroyObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GenerateGame();
        if (Input.GetKeyDown(KeyCode.A))
        {
            _drawer?.SetDirection(Direction.LEFT);
            _drawer?.Move(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _drawer?.SetDirection(Direction.DOWN);
            _drawer?.Move(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _drawer?.SetDirection(Direction.RIGHT);
            _drawer?.Move(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _drawer?.SetDirection(Direction.UP);
            _drawer?.Move(gameObject);
        }
        if (_drawer != null && _drawer.HasFinished())
            GenerateGame();
    }

    private void GenerateGame()
    {
        _drawer?.Destroy();
        var maze = GetRandomMaze();
        _drawer = new GameDrawer(_prefabs, maze);
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
using agent;
using board;
using JetBrains.Annotations;
using ui;
using UnityEngine;

public class GameUI : MonoBehaviour
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
    [SerializeField] public GameObject bigFood;
    [SerializeField] public GameObject pacman;
    [SerializeField] public float speed = 3.00f; // In units per second
    
    
    public delegate void OnGameFinished();
    public static event OnGameFinished OnGameFinishedEvent;
    
    public delegate void OnMazeCreated(int rows, int columns);
    public static event OnMazeCreated OnMazeCreatedEvent;

    [CanBeNull] private GameDrawer _game;
    private Prefabs _prefabs;

    private float deltaTimeSum;

    private void Start()
    {
        OnMazeCreatedEvent?.Invoke(ROWS, COLUMNS);
        var pacmanControl = pacman.GetComponent<PacmanControl>();
        _prefabs = new Prefabs(corridorSquare, wallSquare, foodSquare, bigFood, pacmanControl, InstantiateObject,
            DestroyObject);
        corridorSquare.SetActive(false);
        wallSquare.SetActive(false);
        foodSquare.SetActive(false);
        bigFood.SetActive(false);
        FruitEat.OnFruitEaten += () => { _game?.EatenFruit(); };
        BigFruitEat.OnBigFruitEaten += () => { _game?.EatenBigFruit(); };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GenerateGame();
        if (Input.GetKeyDown(KeyCode.A)) _game?.SetDirection(Direction.LEFT);
        if (Input.GetKeyDown(KeyCode.S)) _game?.SetDirection(Direction.DOWN);
        if (Input.GetKeyDown(KeyCode.D)) _game?.SetDirection(Direction.RIGHT);
        if (Input.GetKeyDown(KeyCode.W)) _game?.SetDirection(Direction.UP);
        deltaTimeSum += Time.deltaTime;
        if (!(deltaTimeSum >= 1 / speed)) return;
        deltaTimeSum = 0;
        MovePacman();
    }

    private void MovePacman()
    {
        _game?.Move();
        if (_game != null && _game.HasFinished())
        {
            _game?.Destroy(); 
            OnGameFinishedEvent?.Invoke();
        }
    }

    private void GenerateGame()
    {
        _game?.Destroy();
        var maze = GetRandomMaze();
        _game = new GameDrawer(_prefabs, maze);
        _game.StartNewGame(gameObject, speed);
        deltaTimeSum = 0;
    }

    private Maze GetRandomMaze()
    {
        var maze = new Maze(ROWS - 2, COLUMNS - 2);
        var initialDonut = new Donut(INITIAL_DONUT_WIDTH, INITIAL_DONUT_HEIGHT);
        var initialPositions = Cycle.Apply(initialDonut, maze);
        var agents = new Agents(MAX_AGENTS, MAX_STEPS_AGENT, WALK_THRESHOLD);
        agents.Walk(maze, initialPositions);
        return maze.Resize(ROWS, COLUMNS);
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
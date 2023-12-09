using agent;
using board;
using JetBrains.Annotations;
using ui;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public delegate void OnGameFinished();

    public delegate void OnMazeCreated(int rows, int columns);

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
    [SerializeField] public Camera cam;
    [SerializeField] public float speed = 3.00f; // In units per second

    [CanBeNull] private GameDrawer _game;
    private Prefabs _prefabs;

    private int bigFoodEaten;
    private float deltaTimeSum;
    private int foodEaten;
    
    private AudioSource _audioSource;

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
        FruitEat.OnFruitEaten += () =>
        {
            _game?.EatenFruit();
            foodEaten += 1;
        };
        BigFruitEat.OnBigFruitEaten += () =>
        {
            _game?.EatenBigFruit();
            bigFoodEaten += 1;
        };
        _audioSource = GetComponent<AudioSource>();
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

    private void OnGUI()
    {
        var style = new GUIStyle
        {
            fontSize = 20
        };
        var x = Screen.width / 2;
        var y = 0;
        var position = new Vector2(x, y);
        var size = new Vector2(200, 200);
        var r = new Rect(position, size);
        GUI.Label(r, "Food eaten: " + foodEaten, style);
        var _y = Screen.height / ROWS * (ROWS - 1) + Screen.height / ROWS / 2;
        GUI.Label(new Rect(x, _y, 200, 200), "Big food eaten: " + bigFoodEaten, style);
    }

    public static event OnGameFinished OnGameFinishedEvent;
    public static event OnMazeCreated OnMazeCreatedEvent;

    private void MovePacman()
    {
        _game?.Move();
        if (_game != null && _game.HasFinished())
        {
            OnGameFinishedEvent?.Invoke();
            GenerateGame();
        }
    }

    private void GenerateGame()
    {
        _audioSource.Stop();
        _game?.Destroy();
        cam.orthographicSize = ROWS / 2.0f;
        var maze = GetRandomMaze();
        _game = new GameDrawer(_prefabs, maze);
        _game.StartNewGame(gameObject, speed);
        deltaTimeSum = 0;
        foodEaten = 0;
        bigFoodEaten = 0;
        _audioSource.Play();
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
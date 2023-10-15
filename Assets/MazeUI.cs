using System;
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
    [SerializeField] public float speed = 3.00f; // In units per second

    [CanBeNull] private GameDrawer _game;
    private Prefabs _prefabs;

    private float deltaTimeSum = 0;

    private void Start()
    {
        var pacmanControl = pacman.GetComponent<PacmanControl>();
        _prefabs = new Prefabs(corridorSquare, wallSquare, foodSquare, pacmanControl, InstantiateObject, DestroyObject);
        corridorSquare.SetActive(false);
        wallSquare.SetActive(false);
        foodSquare.SetActive(false);
        // InvokeRepeating(nameof(MovePacman), 0, 0.33f);
    }

    private void Update()
    {
        deltaTimeSum += Time.deltaTime;
        if (deltaTimeSum >= 1 / speed)
        {
            deltaTimeSum = 0;
            MovePacman();
        }
        if (Input.GetKeyDown(KeyCode.Space)) GenerateGame();
        if (Input.GetKeyDown(KeyCode.A)) _game?.SetDirection(Direction.LEFT);
        if (Input.GetKeyDown(KeyCode.S)) _game?.SetDirection(Direction.DOWN);
        if (Input.GetKeyDown(KeyCode.D)) _game?.SetDirection(Direction.RIGHT);
        if (Input.GetKeyDown(KeyCode.W)) _game?.SetDirection(Direction.UP);
    }

    private void MovePacman()
    {
        _game?.Move();
        if (_game != null && _game.HasFinished())
            GenerateGame();
    }

    private void GenerateGame()
    {
        _game?.Destroy();
        var maze = GetRandomMaze();
        _game = new GameDrawer(_prefabs, maze);
        _game.StartNewGame(gameObject, speed);
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
using System;
using board;
using UnityEngine;

public class PacmanControl : MonoBehaviour
{
    private const string IsMoving = "isMoving";
    private static readonly int Moving = Animator.StringToHash(IsMoving);

    private Animator _an;
    private Vector3? _positionToGo;
    private float _r;
    private float _speed = 3.0f;

    private float _targetAngle;


    private void Start()
    {
        _an = GetComponent<Animator>();
    }

    private void Update()
    {
        RotateSmoothly();
        Move();
    }

    private void RotateSmoothly()
    {
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, _targetAngle, ref _r, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Move()
    {
        if (_positionToGo is { } position && NotInPosition(position))
            Move(position);
        else
            StopMoving();
    }

    private void Move(Vector3 position)
    {
        var step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
        _an.SetBool(Moving, true);
    }

    private void StopMoving()
    {
        _an.SetBool(Moving, false);
    }

    private bool NotInPosition(Vector3 position)
    {
        return Vector3.Distance(transform.position, position) > 0.001f;
    }


    public void InitPacman(Vector2 position, Direction direction, float speed)
    {
        transform.position = position;
        _targetAngle = GetAngle(direction);
        _speed = speed;
    }

    public void GoTo(Vector2 position, Direction direction)
    {
        _positionToGo = position;
        _targetAngle = GetAngle(direction);
    }

    private static float GetAngle(Direction direction)
    {
        return direction switch
        {
            Direction.UP => -90,
            Direction.DOWN => 90,
            Direction.LEFT => -180,
            Direction.RIGHT => 0,
            _ => throw new ArgumentException("Invalid direction")
        };
    }
}
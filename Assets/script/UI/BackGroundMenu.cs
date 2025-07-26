using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackGroundMenu : MonoBehaviour
{
public float speed = 5f; // Tốc độ di chuyển của bóng
    public float changeInterval = 2f; // Thời gian (giây) để đổi hướng

    private Rigidbody2D rb;
    private Vector2 direction; // Hướng di chuyển
    private float timer; // Bộ đếm thời gian

    // Enum định nghĩa các hướng
    private enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetInitialDirection();
        rb.linearVelocity = direction * speed;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            ChangeRandomDirection();
            rb.linearVelocity = direction * speed;
            timer = 0f;
        }
    }

    void SetInitialDirection()
    {
        Direction initialDirection = (Direction)Random.Range(0, System.Enum.GetValues(typeof(Direction)).Length);
        switch (initialDirection)
        {
            case Direction.North:
                direction = new Vector2(0f, 1f);
                break;
            case Direction.NorthEast:
                direction = new Vector2(0.707f, 0.707f);
                break;
            case Direction.East:
                direction = new Vector2(1f, 0f);
                break;
            case Direction.SouthEast:
                direction = new Vector2(0.707f, -0.707f);
                break;
            case Direction.South:
                direction = new Vector2(0f, -1f);
                break;
            case Direction.SouthWest:
                direction = new Vector2(-0.707f, -0.707f);
                break;
            case Direction.West:
                direction = new Vector2(-1f, 0f);
                break;
            case Direction.NorthWest:
                direction = new Vector2(-0.707f, 0.707f);
                break;
        }
        direction = direction.normalized;
    }

    void ChangeRandomDirection()
    {
        Direction newDirection = (Direction)Random.Range(0, System.Enum.GetValues(typeof(Direction)).Length);
        switch (newDirection)
        {
            case Direction.North:
                direction = new Vector2(0f, 1f);
                break;
            case Direction.NorthEast:
                direction = new Vector2(0.707f, 0.707f);
                break;
            case Direction.East:
                direction = new Vector2(1f, 0f);
                break;
            case Direction.SouthEast:
                direction = new Vector2(0.707f, -0.707f);
                break;
            case Direction.South:
                direction = new Vector2(0f, -1f);
                break;
            case Direction.SouthWest:
                direction = new Vector2(-0.707f, -0.707f);
                break;
            case Direction.West:
                direction = new Vector2(-1f, 0f);
                break;
            case Direction.NorthWest:
                direction = new Vector2(-0.707f, 0.707f);
                break;
        }
        direction = direction.normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private Transform leftWaypoint;
    [SerializeField] private Transform rightWaypoint;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    public bool MovingLeft => movingLeft;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("Moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftWaypoint.position.x)
                MoveToDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightWaypoint.position.x)
                MoveToDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("Moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
            SetDirection(movingLeft); // Explicitly set direction when changing
        }
    }

    private void MoveToDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("Moving", true);

        // Set enemy facing direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * -_direction, initScale.y, initScale.z);

        // Move the enemy
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }

    public void SetDirection(bool isMovingLeft)
    {
        movingLeft = isMovingLeft;
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * (movingLeft ? -1 : 1), initScale.y, initScale.z);
    }
}
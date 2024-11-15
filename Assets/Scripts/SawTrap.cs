using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float speed;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = rightPoint.position;
    }

    private void Update()
    {
        // Move saw towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if it reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Switch target to the opposite point
            targetPosition = targetPosition == rightPoint.position ? leftPoint.position : rightPoint.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Call damage or death function on player
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
}

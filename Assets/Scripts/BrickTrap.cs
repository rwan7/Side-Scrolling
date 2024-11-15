using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTrap : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.5f; 
    [SerializeField] private int damage = 1; 
    [SerializeField] private Color gizmoColor = Color.red;
    private bool hasFallen = false; 
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private Collider2D objectCollider;
    [SerializeField] private Collider2D detectionCollider;
    [SerializeField] private AudioClip breakSound;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        objectCollider = GetComponent<Collider2D>();
        detectionCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if player entered the detection area
        if (collision.CompareTag("Player") && !hasFallen)
        {
            Invoke("StartFalling", fallDelay); // Delay before falling
        }
    }

    void StartFalling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic; 
        hasFallen = true; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Break");
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetTrigger("Break");
            SoundManager.instance.PlaySound(breakSound);
        }

        rb.bodyType = RigidbodyType2D.Static;
        objectCollider.enabled = false;
        
    }

    private void OnDrawGizmos()
    {
        if (detectionCollider != null)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireCube(detectionCollider.bounds.center, detectionCollider.bounds.size);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

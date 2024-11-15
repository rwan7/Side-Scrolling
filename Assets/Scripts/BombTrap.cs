using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrap : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private float explosionDelay = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private AudioClip igniteSound;
    [SerializeField] private AudioClip explodeSound;
    private bool triggered = false;
    
    private Animator anim;
    private Collider2D coll2D;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            SoundManager.instance.PlaySound(igniteSound);
            StartCoroutine(ActivateBomb());
        }
    }

    private IEnumerator ActivateBomb()
    {
        anim.SetTrigger("Explode"); 
        yield return new WaitForSeconds(explosionDelay);
        SoundManager.instance.PlaySound(explodeSound);

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, 1.5f); // Adjust radius as needed
        foreach (Collider2D hit in hitObjects)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<Health>().TakeDamage(damage);
            }
        }

        gameObject.SetActive(false);
    }
}


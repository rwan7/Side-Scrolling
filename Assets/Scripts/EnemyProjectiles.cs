using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float projectile_lifetime;
    private float initialY;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (hit) return;

        float movementSpeed = direction * speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        transform.position = new Vector3(transform.position.x, initialY, transform.position.z);

        projectile_lifetime += Time.deltaTime;
        if (projectile_lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("Explode");
            collision.GetComponent<Health>().TakeDamage(1);
        }

        if (collision.tag == "Ground")
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("Explode");
        }
    }

    public void SetDirection(float projectile_direction)
    {
        projectile_lifetime = 0;
        direction = projectile_direction;
        hit = false;
        boxCollider.enabled = true;
        
        transform.rotation = Quaternion.identity;
        float localScaleX = Mathf.Abs(transform.localScale.x) * projectile_direction;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

        initialY = transform.position.y;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}


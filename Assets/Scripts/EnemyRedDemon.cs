using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRedDemon : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    //[SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Ranged Attack Parameters")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float distanceCollider;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Sounds")]
    [SerializeField] private AudioClip shootFireballSound;

    // References
    private Animator anim;
    private EnemyPatrol enemyPatrol;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (CheckPlayer() && cooldownTimer >= attackCooldown)
        {
            Attack();
        }
        else if (!CheckPlayer())
        {
            // Reset trigger if player is no longer detected
            anim.ResetTrigger("Attack");
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !CheckPlayer();
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(shootFireballSound);
        anim.SetTrigger("Attack");

        int fireballIndex = FindFireball();

        fireballs[fireballIndex].transform.position = firepoint.position;
        fireballs[fireballIndex].GetComponent<EnemyProjectiles>().SetDirection(enemyPatrol.MovingLeft ? -1 : 1);
        cooldownTimer = 0;
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            Debug.Log("For jalan");
            if (!fireballs[i].activeInHierarchy)
            {
                Debug.Log("if jalan");
                fireballs[i].SetActive(true);
                return i;
            }
        }
        return 0;
    }

    private bool CheckPlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)); 
    }
}

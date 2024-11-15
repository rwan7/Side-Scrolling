using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Melee Attack")]
    [SerializeField] private float meleeRange = 1f;
    [SerializeField] private float meleeDamage = 10f;
    [SerializeField] private Transform meleePoint;
    [SerializeField] private LayerMask enemyLayers;

    [Header("Sounds")]
    [SerializeField] private AudioClip shootFireballSound;
    [SerializeField] private AudioClip meleeAttackSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && cooldownTimer > attackCooldown)
        {
            RangedAttack();
        }

        if (Input.GetKeyDown(KeyCode.N)) 
        {
            MeleeAttack();
        }  

        cooldownTimer += Time.deltaTime;
    }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(shootFireballSound);
        anim.SetTrigger("Ranged Attack");
        cooldownTimer = 0;

        int fireballIndex = CheckFireball();
        fireballs[fireballIndex].transform.position = firePoint.position;
        fireballs[fireballIndex].GetComponent<Projectiles>().SetDirection(Mathf.Sign(transform.localScale.x)); 
    }

    private void MeleeAttack()
    {
        anim.SetTrigger("Melee Attack");
        SoundManager.instance.PlaySound(meleeAttackSound); 
        DealDamage();
    }

    public void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Health>() != null)
            {
                enemy.GetComponent<Health>().TakeDamage(meleeDamage);
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (meleePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
        }
    }


    private int CheckFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                fireballs[i].SetActive(true);
                return i;
            }
        }
        return 0;
    }
}

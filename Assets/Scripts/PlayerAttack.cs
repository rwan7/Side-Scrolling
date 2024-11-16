using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    private Animator anim;
    private PlayerMovement playerMovement;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private float rangedAttackCooldown = 1;
    private float rangedCooldownTimer = Mathf.Infinity;

    [Header("Melee Attack")]
    [SerializeField] private float meleeRange = 1f;
    [SerializeField] private float meleeDamage = 10f;
    [SerializeField] private Transform meleePoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float meleeAttackCooldown = 0.5f;
    private float meleeCooldownTimer = Mathf.Infinity;

    [Header("Sounds")]
    [SerializeField] private AudioClip shootFireballSound;
    [SerializeField] private AudioClip meleeAttackSound;

    private bool isAttacking = false; // Shared flag for attack state

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        rangedCooldownTimer += Time.deltaTime;
        meleeCooldownTimer += Time.deltaTime;

        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.B) && rangedCooldownTimer >= rangedAttackCooldown)
            {
                StartCoroutine(RangedAttack());
            }

            if (Input.GetKeyDown(KeyCode.N) && meleeCooldownTimer >= meleeAttackCooldown)
            {
                StartCoroutine(MeleeAttack());
            }
        }
    }

    private IEnumerator RangedAttack()
    {
        isAttacking = true;
        SoundManager.instance.PlaySound(shootFireballSound);
        anim.SetTrigger("Ranged Attack");
        rangedCooldownTimer = 0;

        yield return new WaitForSeconds(0.1f); 

        int fireballIndex = CheckFireball();
        fireballs[fireballIndex].transform.position = firePoint.position;
        fireballs[fireballIndex].GetComponent<Projectiles>().SetDirection(Mathf.Sign(transform.localScale.x));

        yield return new WaitForSeconds(rangedAttackCooldown); 
        isAttacking = false;
    }

    private IEnumerator MeleeAttack()
    {
        isAttacking = true;
        anim.SetTrigger("Melee Attack");
        SoundManager.instance.PlaySound(meleeAttackSound);
        meleeCooldownTimer = 0;

        yield return new WaitForSeconds(0.1f); 
        DealDamage();

        yield return new WaitForSeconds(meleeAttackCooldown); 
        isAttacking = false;
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

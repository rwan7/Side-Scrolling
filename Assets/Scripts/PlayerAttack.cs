using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private AudioClip shootFireballSound;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

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
        cooldownTimer += Time.deltaTime;
    }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(shootFireballSound);
        anim.SetTrigger("Ranged Attack");
        cooldownTimer = 0;

        int fireballIndex = CheckFireball();
        fireballs[fireballIndex].transform.position = firePoint.position;
        fireballs[fireballIndex].GetComponent<Projectiles>().SetDirection(Mathf.Sign(transform.localScale.x)); // Set direction after setting position
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

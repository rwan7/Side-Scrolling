using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator anim;
    [SerializeField] private AudioClip deathSound;

    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            Debug.Log("Hurt triggered");
            anim.SetTrigger("Hurt");
        }
        else
        {
            anim.SetTrigger("Die");
            SoundManager.instance.PlaySound(deathSound);

            if (GetComponent<PlayerMovement>() != null)
            {
                GetComponent<PlayerMovement>().enabled = false;
                LoseUIManager.instance.ShowLoseUI();
            }

            if (GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>().enabled = false;

            if (GetComponent<EnemySlime>() != null)
                GetComponent<EnemySlime>().enabled = false;

            if (GetComponent<EnemyRedDemon>() != null)
                GetComponent<EnemyRedDemon>().enabled = false;

            StartCoroutine(DeactivateAfterAnimation());
        }
    }
  

    private IEnumerator DeactivateAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false);
    }


    public void healing(float _heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + _heal, 0, startingHealth);
    }
}

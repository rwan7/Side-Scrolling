using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] private float boostMultiplier = 1.5f; 
    [SerializeField] private float boostDuration = 2f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            player.BoostJump(boostMultiplier, boostDuration);
            gameObject.SetActive(false);
        }
    }
}

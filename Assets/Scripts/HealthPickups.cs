using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickups : MonoBehaviour
{
    [SerializeField] private float heal;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Debug.Log("Player Healed");
            col.GetComponent<Health>().healing(heal);
            gameObject.SetActive(false);
        }
    }
}

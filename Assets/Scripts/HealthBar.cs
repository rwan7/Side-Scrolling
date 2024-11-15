using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHeart;
    [SerializeField] private Image currentHeart;

    // Start is called before the first frame update
    void Start()
    {
        totalHeart.fillAmount = playerHealth.currentHealth/3;
    }

    // Update is called once per frame
    void Update()
    {
        currentHeart.fillAmount = playerHealth.currentHealth/3;
    }
}

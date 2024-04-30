using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public event Action OnDie;

    [SerializeField] private int maxHealth;
    [SerializeField] private Image healthImage;


    private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
        healthImage.fillAmount = 1;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        healthImage.fillAmount = currentHealth / (float)maxHealth;
        if (currentHealth == 0)
        {
            // deD
            Debug.Log("Dead");
            OnDie?.Invoke();
        }
    }
}

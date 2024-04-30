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
        if (healthImage) healthImage.fillAmount = 1;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        if (healthImage) healthImage.fillAmount = currentHealth / (float)maxHealth;
        if (currentHealth == 0)
        {
            OnDie?.Invoke();
        }
    }
}

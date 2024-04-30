using System;
using UnityEngine;

public class ScoreCollectable : MonoBehaviour
{
    public static event Action<int> OnCollect;
    [SerializeField] private int value = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnCollect?.Invoke(value);
            Destroy(gameObject);
        }
    }
}

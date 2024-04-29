using System;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{

    public event Action<Collider2D> TriggerEnter;
    [SerializeField] string tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tag)
        {
            TriggerEnter?.Invoke(collision);
        }

    }

}

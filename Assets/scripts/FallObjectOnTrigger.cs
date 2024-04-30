using UnityEngine;

public class FallObjectOnTrigger : MonoBehaviour
{
    [SerializeField] TriggerEvent triggerEvent;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float gravity;

    private void OnEnable()
    {
        triggerEvent.TriggerEnter += OnEnter;
    }
    private void OnDisable()
    {
        triggerEvent.TriggerEnter -= OnEnter;

    }
    private void Start()
    {
        rb.gravityScale = 0;
    }

    private void OnEnter(Collider2D obj)
    {
        rb.gravityScale = gravity;
    }
}

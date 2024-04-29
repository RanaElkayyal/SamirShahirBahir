using UnityEngine;

public class CanonController : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private HealthBar _health;



    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _health = GetComponentInChildren<HealthBar>();
    }
    private void OnEnable()
    {
        if (_health) _health.OnDie += () => Destroy(gameObject);

    }
    private void OnDisable()
    {
        if (_health) _health.OnDie -= () => Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_particleSystem && !_particleSystem.isPlaying) _particleSystem.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_particleSystem) _particleSystem.Stop();
    }
}

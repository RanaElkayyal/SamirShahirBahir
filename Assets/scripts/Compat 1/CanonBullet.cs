using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : MonoBehaviour
{

    [SerializeField] ParticleSystem _ps;

    private List<ParticleCollisionEvent> collidedParticlesEvents = new List<ParticleCollisionEvent>();


    private void OnParticleCollision(GameObject other)
    {
        int hitNumbs = _ps.GetCollisionEvents(other, collidedParticlesEvents);
        for (int i = 0; i < hitNumbs; i++)
        {
            if (!other.TryGetComponent(out LifeHealth Health)) continue;
            Health.LoseLife();
        }
    }
}

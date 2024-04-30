using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] TriggerEvent triggerEvent;

    [SerializeField] HealthBar myHealth;
    [SerializeField] float speed;
    [SerializeField] float chaceDistance = 4;
    [SerializeField] float attackDistance = 2;
    [SerializeField] float attackCalldown;

    EnemyState currentState = EnemyState.Idle;
    float timeSinceLastAttack;


    private void OnEnable()
    {
        myHealth.OnDie += OnDie;
        triggerEvent.TriggerEnter += ApplyDamage;
    }
    private void OnDisable()
    {
        myHealth.OnDie -= OnDie;
        triggerEvent.TriggerEnter -= ApplyDamage;

    }

    private void ApplyDamage(Collider2D obj)
    {
        obj.GetComponent<LifeHealth>().HitPlayer();
    }

    private void OnDie()
    {
        currentState = EnemyState.Dead;
    }

    private void Update()
    {
        timeSinceLastAttack = timeSinceLastAttack + Time.deltaTime;
        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdleState();
                break;

            case EnemyState.Chace:
                HandleChaceState();
                break;

            case EnemyState.Attack:
                HandleAttackState();
                break;

            case EnemyState.Dead:
                HandleDeadState();
                break;
        }
    }

    private void HandleDeadState()
    {
        Destroy(gameObject);
    }

    private void HandleAttackState()
    {
        if (GetPlayerDistance() > attackDistance)
        {
            currentState = EnemyState.Idle;
            return;
        }

        //attack logic

        if (timeSinceLastAttack > attackCalldown)
        {
            animator.CrossFadeInFixedTime("EnemyAttack", .1f);
            timeSinceLastAttack = 0;
        }
    }

    private void HandleChaceState()
    {
        if (GetPlayerDistance() > chaceDistance)
        {
            currentState = EnemyState.Idle;
            return;
        }

        if (GetPlayerDistance() < attackDistance)
        {
            currentState = EnemyState.Attack;
            return;
        }
        float playerDirec = transform.position.x - player.position.x;
        if (playerDirec > 1)
        {
            rb.velocity = Vector2.left * speed;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (playerDirec < 0)
        {
            rb.velocity = Vector2.right * speed;
            transform.localScale = new Vector3(1, 1, 1);


        }
        animator.SetFloat("Movement", 1);
    }

    private void HandleIdleState()
    {
        //transtions
        if (GetPlayerDistance() < chaceDistance)
        {
            currentState = EnemyState.Chace;
            return;
        }

        //logic
        animator.SetFloat("Movement", 0);
    }

    private float GetPlayerDistance()
    {
        return Mathf.Abs(transform.position.x - player.position.x);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaceDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}

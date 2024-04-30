using UnityEngine;

public class Battary : MonoBehaviour
{
    [SerializeField] float timeAmount;

    LevelTimer timer;

    private void Awake()
    {
        timer = FindObjectOfType<LevelTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timer.AddTime(timeAmount);
            Destroy(gameObject);
        }
    }
}

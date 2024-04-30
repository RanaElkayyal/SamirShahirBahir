using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] float speed;

    bool shouldMove;



    private void Update()
    {
        if (shouldMove)
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        shouldMove = true;
    }


}

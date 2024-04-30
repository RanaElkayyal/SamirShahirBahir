using UnityEngine;

public class Meat : MonoBehaviour
{
    [SerializeField] private int value;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<MeatCollector>().CollectMeat(value);
            Destroy(gameObject);
        }
    }
}


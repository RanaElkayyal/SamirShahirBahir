using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float speed;



    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }
}




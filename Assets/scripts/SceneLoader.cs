using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") SceneManager.LoadScene(sceneIndex);
    }

}

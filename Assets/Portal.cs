using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject window;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(EndLevel());
        }
    }

    private IEnumerator EndLevel()
    {
        anim.CrossFadeInFixedTime("Open", .2f);
        yield return new WaitForSeconds(2);
        window.SetActive(true);
        SceneManager.LoadScene(0);

    }
}

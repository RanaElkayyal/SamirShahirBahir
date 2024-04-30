using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeHealth : MonoBehaviour
{
    [SerializeField] private int lifesNumb;
    [SerializeField] private int hitsToLoseLife;
    [SerializeField] private Image lifesImage;
    [SerializeField] private Sprite[] lifeSprites;

    private int currentHits;

    private int currentLifes;

    private void Start()
    {
        currentLifes = lifesNumb;
    }

    public void HitPlayer()
    {
        currentHits++;
        if (currentHits >= hitsToLoseLife)
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        currentLifes--;
        if (currentLifes <= 0)
        {
            //player dead
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        lifesImage.sprite = lifeSprites[currentLifes - 1];
        currentHits = 0;
    }
}

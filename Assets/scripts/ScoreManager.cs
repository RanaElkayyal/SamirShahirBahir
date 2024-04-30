using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private int currentScore;

    private void OnEnable()
    {
        ScoreCollectable.OnCollect += OnCollect;
    }

    private void Start()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }
    private void OnCollect(int value)
    {
        currentScore += value;
        scoreText.text = currentScore.ToString();
    }

    private void OnDisable()
    {
        ScoreCollectable.OnCollect -= OnCollect;

    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver { get; private set; }

    private int score;

    public TextMeshProUGUI scroeText;
    public GameObject gameOverUi;

    private void Awake()
    {
        isGameOver = false;
        gameOverUi.SetActive(false);
    }

    private void Update()
    {
        if (!isGameOver)
            return;

        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddScore(int add)
    {
        if (!isGameOver)
        {
            score += add;
            scroeText.text = $"Score: {score}";
        }
    }
    public void OnPlayerDead()
    {
        isGameOver = true;
        gameOverUi.SetActive(true);
    }

}

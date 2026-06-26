using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // ゲーム開始処理
    public void StartGame()
    {
        Score = 0;
        SceneManager.LoadScene("GameScene");
    }
    public void GameFinish()
    {
        SceneManager.LoadScene("ScoreScene");
    }
}

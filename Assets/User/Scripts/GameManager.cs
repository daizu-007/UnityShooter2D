using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score { get; set; }

    // ハイスコアを PlayerPrefs で永続化して保持
    private const string HighScoreKey = "HighScore";
    public int HighScore
    {
        get => PlayerPrefs.GetInt(HighScoreKey, 0);
        private set => PlayerPrefs.SetInt(HighScoreKey, value);
    }

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
        // 今回のスコアがハイスコアを上回っていれば更新して保存
        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("ScoreScene");
    }
}

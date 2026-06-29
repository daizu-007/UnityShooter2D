using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private TMPro.TMP_Text scoreText;
    private TMPro.TMP_Text maxScoreText;

    void Start()
    {
        scoreText = GetComponent<TMPro.TMP_Text>();
        if (scoreText == null)
        {
            Debug.LogError("ScoreManager: TMP_Text コンポーネントが見つかりません。");
        }

        // 同じ Canvas 内の "MaxScore" オブジェクトからハイスコア表示用テキストを取得
        maxScoreText = GameObject.Find("MaxScore")?.GetComponent<TMPro.TMP_Text>();

        if (GameManager.Instance == null)
        {
            if (scoreText != null) scoreText.text = "Score: 0";
            if (maxScoreText != null) maxScoreText.text = "HighScore: 0";
            return;
        }

        int score = GameManager.Instance.Score;        // 今回のスコア
        int highScore = GameManager.Instance.HighScore; // 歴代ハイスコア

        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
        if (maxScoreText != null)
        {
            maxScoreText.text = $"HighScore: {highScore}";
        }
    }
}

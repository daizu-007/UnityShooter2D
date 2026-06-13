using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private bool isRandomMoving = false;
    private Vector3 randomMoveGoal;

    void Update()
    {
        if (isRandomMoving)
        {
            // 目標の座標に向かって移動
            float step = moveSpeed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, randomMoveGoal, step);
            if (Mathf.Approximately(this.transform.position.x, randomMoveGoal.x) && Mathf.Approximately(this.transform.position.y, randomMoveGoal.y))
            {
                isRandomMoving = false; // 目標に到達したらランダム移動を終了
            }
        }
        else
        {
            // ランダムな座標を目標に設定
            float height = Camera.main.orthographicSize; // 画面の縦幅を計算
            float width = height * Camera.main.aspect; // 画面の横幅を計算
            randomMoveGoal = new Vector3(Random.Range(width * -1f, width), Random.Range(height * -1f, height), this.transform.position.z); // 画面内のランダムな座標
            isRandomMoving = true;
        }
    }
}

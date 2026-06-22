using UnityEngine;

[CreateAssetMenu(fileName = "RandomMove", menuName = "EnemyPatterns/RandomMove")]
public class RandomMove : MovePattern
{
    private bool isRandomMoving = false;
    private Vector3 randomMoveGoal;

    public override void Tick(Transform self, float moveSpeed)
    {
        if (isRandomMoving)
        {
            // 目標の座標に向かって移動
            float step = moveSpeed * Time.deltaTime;
            self.position = Vector3.MoveTowards(self.position, randomMoveGoal, step);
            if (Mathf.Approximately(self.position.x, randomMoveGoal.x) && Mathf.Approximately(self.position.y, randomMoveGoal.y))
            {
                isRandomMoving = false; // 目標に到達したらランダム移動を終了
            }
        }
        else
        {
            // ランダムな座標を目標に設定
            float height = Camera.main.orthographicSize; // 画面の縦幅を計算
            float width = height * Camera.main.aspect; // 画面の横幅を計算
            randomMoveGoal = new Vector3(Random.Range(width * -1f, width), Random.Range(height * -1f, height), self.position.z); // 画面内のランダムな座標
            isRandomMoving = true;
        }
    }
}

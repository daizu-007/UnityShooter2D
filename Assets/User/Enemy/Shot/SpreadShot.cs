using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "SpreadShot", menuName = "EnemyPatterns/SpreadShot")]
public class SpreadShot : ShotPattern
{
    public override void Fire(Transform self, float bulletSpeed, IObjectPool<BulletBase> pool)
    {
        // 360度に弾を発射する
        for(int i = 0; i < 8; i++)
        {
            float angle = i * 45f; // 8方向に分けるので45度ずつ
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.down; // 下方向を基準に回転
            Vector3 bulletSpawnPosition = self.position + direction * 0.5f; // 弾の発射位置を調整
            BulletBase bullet = pool.Get(); // プールから弾を取得
            bullet.transform.position = bulletSpawnPosition;
            bullet.transform.up = direction;
        }
    }
}

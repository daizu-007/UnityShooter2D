using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "DownSingleShot", menuName = "EnemyPatterns/DownSingleShot")]
public class DownSingleShot : ShotPattern
{
    public override void Fire(Transform self, float bulletSpeed, IObjectPool<BulletBase> pool)
    {
        // 画面下に向かって弾を発射
        Vector3 bulletSpawnPosition = self.position + Vector3.down * 0.5f; // 弾の発射位置を調整
        BulletBase bullet = pool.Get(); // プールから弾を取得
        bullet.transform.position = bulletSpawnPosition;
    }
}

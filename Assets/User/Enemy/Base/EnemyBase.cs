using UnityEngine;
using UnityEngine.Pool;

public abstract class MovePattern : ScriptableObject
{
    public abstract void Tick(Transform self, float moveSpeed);
}

public abstract class ShotPattern : ScriptableObject
{
    public abstract void Tick(Transform self, float bulletSpeed, IObjectPool<BulletBase> pool, float fireRate);
}

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private MovePattern movePattern;
    [SerializeField] private ShotPattern shotPattern;
    [SerializeField] private BulletBase bulletPrefab;
    private ObjectPool<BulletBase> pool;
    void Awake()
    {
        pool = new ObjectPool<BulletBase>(
            // プールが空で新しく作るとき
            createFunc: () =>
            {
                BulletBase bullet = Instantiate(bulletPrefab);
                bullet.SetPool(pool); // プールの参照を渡す
                return bullet;
            },
            // プールから取り出すとき
            actionOnGet: (bullet) => bullet.gameObject.SetActive(true),
            // プールに戻すとき
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            // 破棄するとき
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            defaultCapacity: 30
        );
    }
    protected virtual void Update()
    {
        movePattern?.Tick(transform, moveSpeed);
        shotPattern?.Tick(transform, bulletSpeed, pool, fireRate);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }


}

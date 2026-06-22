using UnityEngine;
using UnityEngine.Pool;

public abstract class BulletPattern : ScriptableObject
{
    public abstract void Tick(Transform self, float bulletSpeed);
}

public class BulletBase : MonoBehaviour
{
    [SerializeField] private BulletPattern bulletPattern;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private IObjectPool<BulletBase> pool;
    public void SetPool(IObjectPool<BulletBase> pool)
    {
        this.pool = pool;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage); // プレイヤーにダメージを与える
            }
            pool.Release(this); // 弾をプールに戻す
        }
    }
    protected virtual void Update()
    {
        bulletPattern.Tick(transform, speed);
    }
}

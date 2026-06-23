using UnityEngine;
using UnityEngine.Pool;

public abstract class BulletPattern : ScriptableObject
{
    public abstract void Tick(Transform self, float bulletSpeed);
}

public class BulletBase : MonoBehaviour
{
    [SerializeField] private BulletPattern bulletPattern;
    [SerializeField] private float damage;
    private float speed = 0f;
    private IObjectPool<BulletBase> pool;
    public void Initialize(float speed)
    {
        this.speed = speed;
    }
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
        // 画面外から出たら削除する
        float height = Camera.main.orthographicSize; // 画面の縦幅を計算
        float width = height * Camera.main.aspect; // 画面の横幅を計算
        if (transform.position.x < -width || transform.position.x > width || transform.position.y < -height || transform.position.y > height)
        {
            pool.Release(this);
        }
    }
}

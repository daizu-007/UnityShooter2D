using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private IObjectPool<BulletController> pool;
    private bool isReleased;

    public void SetPool(IObjectPool<BulletController> pool)
    {
        this.pool = pool;
    }

    void OnEnable()
    {
        isReleased = false;
    }

    void Despawn()
    {
        if (isReleased || pool == null)
        {
            return;
        }

        isReleased = true;
        pool.Release(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // タグ設定に依存せず、EnemyBase コンポーネントの有無で判定する
        if (other.TryGetComponent<EnemyBase>(out var enemy))
        {
            enemy.TakeDamage(damage); // 敵にダメージを与える
            Despawn(); // 弾をプールに戻す
        }
    }

    void Update()
    {
        this.transform.position += (Vector3)(Vector2.up * speed * Time.deltaTime);
        // 画面外から出たら削除する
        float height = Camera.main.orthographicSize; // 画面の縦幅を計算
        float width = height * Camera.main.aspect; // 画面の横幅を計算
        if (transform.position.x < -width || transform.position.x > width || transform.position.y < -height || transform.position.y > height)
        {
            Despawn();
        }
    }
}

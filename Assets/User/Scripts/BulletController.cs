using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private IObjectPool<BulletController> pool;
    public void SetPool(IObjectPool<BulletController> pool)
    {
        this.pool = pool;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet collided with: " + other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // 敵にダメージを与える
            }
            pool.Release(this); // 弾をプールに戻す
        }
    }

    void Update()
    {
        this.transform.position += (Vector3)(Vector2.up * speed * Time.deltaTime);
        float topBound = Camera.main.orthographicSize + 1f; // 画面上端+オーバーランで削除
        if (this.transform.position.y > topBound)
        {
            pool.Release(this);
        }
    }
}

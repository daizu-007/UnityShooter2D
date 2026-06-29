using UnityEngine;
using UnityEngine.Pool;

public interface IMover
{
    void Tick(Transform self, float moveSpeed);
}
public abstract class MovePattern : ScriptableObject
{
    public abstract IMover CreateMover();
}

public abstract class ShotPattern : ScriptableObject
{
    public abstract void Fire(Transform self, float bulletSpeed, IObjectPool<BulletBase> pool);
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
    private float lastShotTime = 0f;
    private IMover mover;
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
            actionOnGet: (bullet) => 
            {
                bullet.gameObject.SetActive(true); 
                bullet.Initialize(bulletSpeed); // 弾の初期化
            },
            // プールに戻すとき
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            // 破棄するとき
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            defaultCapacity: 30
        );
    }

    protected virtual void Update()
    {
        mover.Tick(transform, moveSpeed);

        if (Time.time >= lastShotTime + fireRate)
        {
            shotPattern?.Fire(transform, bulletSpeed, pool);
            lastShotTime = Time.time;
        }

        // HPに応じて色を変える
        Color color = Color.Lerp(Color.red, Color.green, hp / 100f); // HPが0のとき赤、100のとき緑
        GetComponent<SpriteRenderer>().color = color;
    }

    void Die()
    {
        GameManager.Instance.Score += 10; // 得点を加算
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

    public void Initialize(float hp, float moveSpeed, float bulletSpeed, float fireRate, MovePattern movePattern, ShotPattern shotPattern, BulletBase bulletPrefab)
    {
        this.hp = hp;
        this.moveSpeed = moveSpeed;
        this.bulletSpeed = bulletSpeed;
        this.fireRate = fireRate;
        this.movePattern = movePattern;
        this.shotPattern = shotPattern;
        this.bulletPrefab = bulletPrefab;
        mover = this.movePattern.CreateMover();
        this.gameObject.tag = "Enemy";
    }
}

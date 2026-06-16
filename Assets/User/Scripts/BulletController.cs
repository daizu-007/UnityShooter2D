using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private IObjectPool<BulletController> pool;
    public void SetPool(IObjectPool<BulletController> pool)
    {
        this.pool = pool;
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

using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        this.transform.position += (Vector3)(Vector2.up * speed * Time.deltaTime);
        float topBound = Camera.main.orthographicSize + 1f; // 画面上端+オーバーランで削除
        if (this.transform.position.y > topBound)
        {
            Destroy(this.gameObject);
        }
    }
}

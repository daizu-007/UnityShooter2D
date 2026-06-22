using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerBulletSpawner : MonoBehaviour
{
    [SerializeField] private BulletController bulletPrefab; // 弾のプレハブ
    [SerializeField] private float fireRate; // 連射間隔（秒）

    private PlayerInput playerInput;
    private InputAction fireAction;
    private bool isFiring; // 発射ボタンが押されているか
    private float lastFireTime = 0f; // 最後に発射した時間
    private ObjectPool<BulletController> pool;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"];
        pool = new ObjectPool<BulletController>(
            // プールが空で新しく作るとき
            createFunc: () =>
            {
                BulletController bullet = Instantiate(bulletPrefab);
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
    
    // オブジェクトがゲーム内で有効になったとき
    void OnEnable()
    {
        fireAction.canceled += OnFireReleased; // fireAction.canceledが実行されるときにOnFireReleasedメソッドを呼び出すように登録
    }

    // オブジェクトがゲーム内で無効になったとき
    void OnDisable()
    {
        fireAction.canceled -= OnFireReleased; // イベントの購読を解除してメモリリークを防止
    }

    void OnFireReleased(InputAction.CallbackContext context)
    {
        isFiring = false; // 発射ボタンが離されたときにフラグを下げる
    }

    void OnFire(InputValue fireValue)
    {
        isFiring = true;
    }
    void Update()
    {
        // 弾を発射
        if (isFiring && Time.time >= lastFireTime + fireRate)
        {
            BulletController bullet = pool.Get();
            bullet.transform.position = this.transform.position;
            lastFireTime = Time.time;
        }
    }
}

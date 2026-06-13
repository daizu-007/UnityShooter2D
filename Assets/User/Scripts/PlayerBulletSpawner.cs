using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // 弾のプレハブ
    [SerializeField] private float fireRate; // 連射間隔（秒）

    private PlayerInput playerInput;
    private InputAction fireAction;
    private bool isFiring; // 発射ボタンが押されているか
    private float lastFireTime; // 最後に発射した時間

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"];
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 弾を発射
        if (isFiring && Time.time >= lastFireTime + fireRate)
        {
            Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            lastFireTime = Time.time;
        }
    }
}

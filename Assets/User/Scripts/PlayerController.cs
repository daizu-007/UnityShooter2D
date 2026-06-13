using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{
    [SerializeField] private float moveSpeed; // プレイヤーの移動速度
    [SerializeField] private float screenPadding; // 画面端からのオフセット（プレイヤーが見切れない用）
    private Vector2 clampedMoveInput;
    private float maxHeight;
    private float maxWidth;

    void Start()
    {
        UpdateBounds(); // 初回の画面範囲を設定
    }

    // 入力時
    void OnMove(InputValue movementValue)
    {
        Vector2 moveInput = movementValue.Get<Vector2>();
        clampedMoveInput = Vector2.ClampMagnitude(moveInput, 1f); // 入力の大きさを1に制限
    }

    void UpdateBounds()
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize; // 2Dカメラの縦半分
        float halfWidth = halfHeight * cam.aspect; // 画面アスペクト比から横半分を算出

        maxHeight = halfHeight - screenPadding;
        maxWidth = halfWidth - screenPadding;
    }

    void Update()
    {
        // 解像度変わった場合に備えて毎フレーム更新（負荷が気になる場合は UpdateBounds() を Start のみに）
        UpdateBounds();

        Vector3 newPos = this.transform.position + (Vector3)(clampedMoveInput * moveSpeed * Time.deltaTime);

        // 範囲制限: 画面サイズ以内にクランプ
        newPos.x = Mathf.Clamp(newPos.x, -maxWidth, maxWidth);
        newPos.y = Mathf.Clamp(newPos.y, -maxHeight, maxHeight);

        this.transform.position = newPos;
    }
}

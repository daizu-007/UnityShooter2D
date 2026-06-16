using UnityEngine;

public abstract class MovePattern : ScriptableObject
{
    public abstract void Tick(Transform self);
}

public abstract class ShotPattern : ScriptableObject
{
    public abstract void Tick(Transform self);
}

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] private MovePattern movePattern;
    [SerializeField] private ShotPattern shotPattern;

    protected virtual void Update()
    {
        movePattern?.Tick(transform);
        shotPattern?.Tick(transform);
    }

    public void TakeDamage(int dmg) { /* 共通の被弾処理 */ }


}

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
    [SerializeField] protected float hp;
    [SerializeField] private MovePattern movePattern;
    [SerializeField] private ShotPattern shotPattern;

    protected virtual void Update()
    {
        movePattern?.Tick(transform);
        shotPattern?.Tick(transform);
    }

    void Die()
    {
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


}

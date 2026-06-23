using UnityEngine;

[CreateAssetMenu(fileName = "SimpleDown", menuName = "BulletPatterns/SimpleDown")]
public class SimpleDown : BulletPattern
{
    public override void Tick(Transform self, float bulletSpeed)
    {
        self.position += Vector3.down * bulletSpeed * Time.deltaTime;
    }
}

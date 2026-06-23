using UnityEngine;

[CreateAssetMenu(fileName = "SimpleGo", menuName = "BulletPatterns/SimpleGo")]
public class SimpleGo : BulletPattern
{
    public override void Tick(Transform self, float bulletSpeed)
    {
        self.position += self.up * bulletSpeed * Time.deltaTime;
    }
}

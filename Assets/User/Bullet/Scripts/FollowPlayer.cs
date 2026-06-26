using UnityEngine;

[CreateAssetMenu(fileName = "FollowPlayer", menuName = "BulletPatterns/FollowPlayer")]
public class FollowPlayer : BulletPattern
{
    [SerializeField, Min(0f)]
    private float turnSpeed = 180f;

    public override void Tick(Transform self, float bulletSpeed)
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject == null)
        {
            return;
        }

        Vector3 direction = (playerObject.transform.position - self.position).normalized;
        Vector3 nextDirection = Vector3.RotateTowards(
            self.up,
            direction,
            turnSpeed * Mathf.Deg2Rad * Time.deltaTime,
            0f
        );

        self.up = nextDirection;
        self.position += nextDirection * bulletSpeed * Time.deltaTime;
    }
}

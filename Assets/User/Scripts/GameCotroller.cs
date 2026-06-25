using UnityEngine;
using System.Collections.Generic;

public class GameCotroller : MonoBehaviour
{
    [System.Serializable]
    private struct FloatRange
    {
        public float min;
        public float max;
    }

    [SerializeField] private int maxEnemyCount;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;
    [SerializeField] private EnemyBase enemyPrefab;
    [SerializeField] private List<MovePattern> movePatterns;
    [SerializeField] private List<ShotPattern> shotPatterns;
    [SerializeField] private List<BulletBase> bulletPrefabs;
    [SerializeField] private FloatRange hpRange;
    [SerializeField] private FloatRange speedRange;
    [SerializeField] private FloatRange bulletSpeedRange;
    [SerializeField] private FloatRange fireRateRange;
    private float lastSpawnTime;
    void SpawnEnemy()
    {
        float hp = Random.Range(hpRange.min, hpRange.max);
        float speed = Random.Range(speedRange.min, speedRange.max);
        float bulletSpeed = Random.Range(bulletSpeedRange.min, bulletSpeedRange.max);
        float fireRate = Random.Range(fireRateRange.min, fireRateRange.max);
        MovePattern movePattern = movePatterns[Random.Range(0, movePatterns.Count)];
        ShotPattern shotPattern = shotPatterns[Random.Range(0, shotPatterns.Count)];
        BulletBase bulletPrefab = bulletPrefabs[Random.Range(0, bulletPrefabs.Count)];
        EnemyBase enemy = Instantiate(enemyPrefab);
        enemy.Initialize(hp, speed, bulletSpeed, fireRate, movePattern, shotPattern, bulletPrefab); 
    }

    void Start()
    {
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime >= Random.Range(minSpawnInterval, maxSpawnInterval))
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemyCount)
            {
                SpawnEnemy();
                lastSpawnTime = Time.time;
            }
        }
    }
}

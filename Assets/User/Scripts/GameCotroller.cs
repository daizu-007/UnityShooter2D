using UnityEngine;
using System.Collections.Generic;

public class GameCotroller : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyPrefab;
    [SerializeField] private List<MovePattern> movePatterns;
    [SerializeField] private List<ShotPattern> shotPatterns;
    [SerializeField] private List<BulletBase> bulletPrefabs;
    void SpawnEnemy()
    {
        float hp = Random.Range(50f, 100f);
        float speed = Random.Range(1f, 3f);
        float bulletSpeed = Random.Range(4f, 6f);
        float fireRate = Random.Range(0.1f, 1.0f);
        MovePattern movePattern = movePatterns[Random.Range(0, movePatterns.Count)];
        ShotPattern shotPattern = shotPatterns[Random.Range(0, shotPatterns.Count)];
        BulletBase bulletPrefab = bulletPrefabs[Random.Range(0, bulletPrefabs.Count)];
        EnemyBase enemy = Instantiate(enemyPrefab);
        enemy.Initialize(hp, speed, bulletSpeed, fireRate, movePattern, shotPattern, bulletPrefab); 
    }

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        
    }
}

using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<Enemy> EnemyList = new List<Enemy>();

    public void Initialize()
    {
        SpawnMonster();
    }

    public void SpawnMonster()
    {
        for (int i = 0; i < 10; i++)
        {
            Enemy newEnemy = Manager.Pool.GetEnemy();

            Vector3 spawnPosition = GetRandomCircleSpawnPosition();
            newEnemy.Initialize();
            newEnemy.SetPosition(spawnPosition);

            EnemyList.Add(newEnemy);
        }
    }

    private Vector3 GetRandomCircleSpawnPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        Vector3 spawnDirection = new Vector3(
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0
        );

        Vector3 spawnPosition = Manager.Data.Player.transform.position
                                + spawnDirection * Manager.Data.SpawnDistance;

        return spawnPosition;
    }
}

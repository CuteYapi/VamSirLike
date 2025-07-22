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
            Enemy newEnemy = Instantiate(Manager.Data.RefEnemy);

            int randomXPosition = Random.Range(-4, 5);
            int randomYPosition = Random.Range(-4, 5);

            newEnemy.transform.localPosition = new Vector3(randomXPosition, randomYPosition, 0);

            EnemyList.Add(newEnemy);
        }
    }
}

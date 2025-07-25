using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PoolManager : MonoBehaviour
{
    public List<Enemy> EnemyPool = new List<Enemy>();

    public Enemy GetEnemy()
    {
        // 1. 풀 안에 사용할 수 있는 객체가 있는가?
        // 1-1. 반복문을 사용한다
        /*
        for (int i = 0; i < EnemyPool.Count; i++)
        {
            if (EnemyPool[i].gameObject.activeSelf == false)
            {
                return EnemyPool[i];
            }
        }
        */

        // 1-2. Linq 를 사용한다
        Enemy targetEnemy = EnemyPool.FirstOrDefault(target => target.gameObject.activeSelf == false);
        if (targetEnemy != null)
        {
            return targetEnemy;
        }


        // 2. 만약 없다면 생성 후 풀에 추가하고 반환
        Enemy refEnemy = Manager.Data.RefEnemy;
        targetEnemy = Instantiate(refEnemy);

        EnemyPool.Add(targetEnemy);

        return targetEnemy;
    }

    public List<HpBarView> HpBarViewPool = new List<HpBarView>();
    public HpBarView GetHpBarView()
    {
        HpBarView target = HpBarViewPool.FirstOrDefault(target => target.gameObject.activeSelf == false);
        if (target != null)
        {
            return target;
        }


        // 2. 만약 없다면 생성 후 풀에 추가하고 반환
        HpBarView refHpBar = Manager.Data.RefHpBarView;
        target = Instantiate(refHpBar);
        target.Initialize();

        HpBarViewPool.Add(target);

        return target;
    }

    public List<DamageView> DamageViewPool = new List<DamageView>();
    public DamageView GetDamageView()
    {
        DamageView target = DamageViewPool.FirstOrDefault(target => target.gameObject.activeSelf == false);
        if (target != null)
        {
            return target;
        }


        // 2. 만약 없다면 생성 후 풀에 추가하고 반환
        DamageView refDamageView = Manager.Data.RefDamageView;
        target = Instantiate(refDamageView);
        target.Initialize();

        DamageViewPool.Add(target);

        return target;
    }
}

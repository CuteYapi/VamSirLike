using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldToScreenController : MonoBehaviour, IUI
{
    public float ResetTime = 0.2f;
    public Dictionary<Enemy, HpBarView> EnemyHpBarViewDictionary = new Dictionary<Enemy, HpBarView>();


    private Dictionary<Enemy, Vector3> mEnemyScreenPositionDictionary = new Dictionary<Enemy, Vector3>();

    private Coroutine mCoResetPosition = null;

    #region 공통 부분 : 상송 받아 정의하는 부분

    public void Initialize()
    {

    }

    public void Open()
    {
        gameObject.SetActive(true);

        if (mCoResetPosition == null)
        {
            mCoResetPosition = StartCoroutine(CoResetPositionDic());
        }
    }

    public void Close()
    {
        if (mCoResetPosition != null)
        {
            StopCoroutine(mCoResetPosition);
            mCoResetPosition = null;
        }

        gameObject.SetActive(false);
    }

    private IEnumerator CoResetPositionDic()
    {
        var waitTime = new WaitForSeconds(ResetTime);

        while (true)
        {
            mEnemyScreenPositionDictionary.Clear();
            yield return waitTime;
        }
    }

    #endregion

    #region Unity Event 함수

    private void Update()
    {
        SetHpBar();
    }

    private Vector3 GetScreenPosition(Enemy target)
    {
        if (mEnemyScreenPositionDictionary.TryGetValue(target, out Vector3 screenPosition) == false)
        {
            screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
            mEnemyScreenPositionDictionary.Add(target, screenPosition);
        }

        return screenPosition;
    }

    private void SetHpBar()
    {
        List<Enemy> enemyList = Manager.Stage.EnemyList;

        foreach (Enemy enemy in enemyList)
        {
            if (enemy.IsDead == true)
            {
                continue;
            }

            if (EnemyHpBarViewDictionary.TryGetValue(enemy, out HpBarView hpView) == false)
            {
                hpView = Manager.Pool.GetHpBarView();
                hpView.transform.SetParent(transform);
                hpView.Open();
                hpView.SetSliderRatio(enemy.HpRatio);

                EnemyHpBarViewDictionary.Add(enemy, hpView);
            }

            Vector3 screenPosition = GetScreenPosition(enemy);
            hpView.SetPosition(screenPosition);
        }
    }


    public void SetDamageView(Enemy enemy, int value)
    {
        Vector3 targetPosition = GetScreenPosition(enemy);
        DamageView damageView = Manager.Pool.GetDamageView();
        damageView.SetText(value);
        damageView.SetPosition(targetPosition);
        damageView.transform.SetParent(transform);
        damageView.Open();
    }
    #endregion
}

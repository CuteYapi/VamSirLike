using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldToScreenController : MonoBehaviour, IUI
{
    public float ResetTime = 0.2f;

    private Coroutine mCoResetPosition;

    private Dictionary<Enemy, Vector3> mEnemyScreenPositionDictionary = new Dictionary<Enemy, Vector3>();

    public void Initialize()
    {

    }

    public void Open()
    {
        mCoResetPosition = StartCoroutine(CoResetPositionDic());
    }

    public void Close()
    {
        if (mCoResetPosition != null)
        {
            StopCoroutine(mCoResetPosition);
            mCoResetPosition = null;
        }
    }

    public void SetHpBar()
    {

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

    private Vector3 GetScreenPosition(Enemy target)
    {
        if (mEnemyScreenPositionDictionary.TryGetValue(target, out Vector3 screenPosition) == false)
        {
            screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
            mEnemyScreenPositionDictionary.Add(target, screenPosition);
        }

        return screenPosition;
    }
}

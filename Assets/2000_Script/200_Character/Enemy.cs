using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public EnemyStatus Status;

    public int CurrentHp { get; private set; }

    public float HpRatio
    {
        get => (float)CurrentHp / Status.MaxHp;
    }

    public bool IsDead
    {
        get => CurrentHp <= 0;
    }

    public string Name;

    public EnemyType Type;

    private Color32 mDamagedColor = Color.red;
    private Color32 mMainColor = Color.white;

    private WaitForSeconds mDamagedSwapTime = new WaitForSeconds(0.2f);

    private SpriteRenderer mRenderer;

    private Coroutine mCoDamagedFx;

    #region Unity Event
    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Player player = Manager.Data.Player;
        if (player == null)
        {
            return;
        }

        Vector3 direction = (player.transform.position - transform.position).normalized;

        transform.localPosition += direction * Status.MoveSpeed * Time.deltaTime;
    }

    #endregion

    public void Initialize()
    {
        gameObject.SetActive(true);

        mRenderer = transform.GetComponent<SpriteRenderer>();

        Type = EnemyType.Slime;

        // 능력치 세팅이 원래는 데이터 테이블 참조해서 세팅
        Status.TempStatusInit();
        GetHeal(Status.MaxHp);
    }


    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void SetName(string text)
    {
        Name = Type.ToString() + text;
    }


    #region HP 작업

    public void GetDamaged(int damage)
    {
        CurrentHp -= damage;

        if (UI.WorldToScreen.EnemyHpBarViewDictionary.TryGetValue(this, out HpBarView hpBar) == true)
        {
            hpBar.SetSliderRatio(HpRatio);
        }
#if Log
        else
        {
            Log.Error(LogType.UI_WorldToScreen, $"WorldToScreen에 HpBarView 가 세팅되지 않았습니다!");
        }
#endif

        UI.WorldToScreen.SetDamageView(this, damage);

        if (IsDead == true)
        {
            Died();

            return;
        }
#if Log
        else
        {
            Log.Message(LogType.StatHp, $"{Name} 공격 받음 남은 체력 :{CurrentHp}");
        }
#endif

        if (mCoDamagedFx != null)
        {
            StopCoroutine(mCoDamagedFx);
        }
        mCoDamagedFx = StartCoroutine(CoDamagedFx());
    }

    private IEnumerator CoDamagedFx()
    {
        mRenderer.color = mDamagedColor;
        yield return mDamagedSwapTime;

        mRenderer.color = mMainColor;
        yield return mDamagedSwapTime;

        mRenderer.color = mDamagedColor;
        yield return mDamagedSwapTime;

        mRenderer.color = mMainColor;
        yield return mDamagedSwapTime;

        mRenderer.color = mDamagedColor;
        yield return mDamagedSwapTime;

        mRenderer.color = mMainColor;

        mCoDamagedFx = null;
    }

    public void GetHeal(int amount)
    {
        CurrentHp += amount;

        if (CurrentHp > Status.MaxHp)
        {
            CurrentHp = Status.MaxHp;
        }

        Log.Message(LogType.StatHp, $"{Name} 치유 받음 현재 체력 :{CurrentHp}");
    }

    private void Died()
    {
        CurrentHp = 0;

        StopAllCoroutines();

        gameObject.SetActive(false);

#if Log
        Log.Message(LogType.StatHp, $"{Name} 사망!");
#endif

        if (UI.WorldToScreen.EnemyHpBarViewDictionary.TryGetValue(this, out HpBarView hpBar) == true)
        {
            hpBar.Close();
            UI.WorldToScreen.EnemyHpBarViewDictionary.Remove(this);
        }
#if Log
        else
        {
            Log.Error(LogType.UI_WorldToScreen, $"WorldToScreen에 HpBarView 가 세팅되지 않았습니다!");
        }
#endif
    }

    #endregion
}

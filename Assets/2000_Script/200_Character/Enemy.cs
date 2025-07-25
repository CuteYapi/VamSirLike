using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStatus Status;

    public Rigidbody2D rigidBody;

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

        Vector2 direction = (player.transform.position - transform.position).normalized;

        rigidBody.linearVelocity = direction * Status.MoveSpeed;
    }

    #endregion

    public void Initialize()
    {
        gameObject.SetActive(true);

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
        }
#if Log
        else
        {
            Log.Message(LogType.StatHp, $"{Name} 공격 받음 남은 체력 :{CurrentHp}");
        }
#endif
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

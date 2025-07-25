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

        // �ɷ�ġ ������ ������ ������ ���̺� �����ؼ� ����
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


    #region HP �۾�

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
            Log.Error(LogType.UI_WorldToScreen, $"WorldToScreen�� HpBarView �� ���õ��� �ʾҽ��ϴ�!");
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
            Log.Message(LogType.StatHp, $"{Name} ���� ���� ���� ü�� :{CurrentHp}");
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

        Log.Message(LogType.StatHp, $"{Name} ġ�� ���� ���� ü�� :{CurrentHp}");
    }

    private void Died()
    {
        CurrentHp = 0;
        gameObject.SetActive(false);

#if Log
        Log.Message(LogType.StatHp, $"{Name} ���!");
#endif

        if (UI.WorldToScreen.EnemyHpBarViewDictionary.TryGetValue(this, out HpBarView hpBar) == true)
        {
            hpBar.Close();
            UI.WorldToScreen.EnemyHpBarViewDictionary.Remove(this);
        }
#if Log
        else
        {
            Log.Error(LogType.UI_WorldToScreen, $"WorldToScreen�� HpBarView �� ���õ��� �ʾҽ��ϴ�!");
        }
#endif
    }

    #endregion
}

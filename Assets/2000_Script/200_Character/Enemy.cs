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

        // �ɷ�ġ ������ ������ ������ ���̺� �����ؼ� ����
        Status.TempStatusInit();
        GetHeal(Status.MaxHp);
    }


    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }



    #region HP �۾�

    public void GetDamaged(int damage)
    {
        CurrentHp -= damage;

        if (IsDead == true)
        {
            CurrentHp = 0;
            Log.Message(LogType.StatHp, $"{this.name} ���!");
        }
#if Log
        else
        {
            Log.Message(LogType.StatHp, $"{this.name} ���� ���� ���� ü�� :{CurrentHp}");
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

        Log.Message(LogType.StatHp, $"{this.name} ġ�� ���� ���� ü�� :{CurrentHp}");
    }

    private void Died()
    {
        
    }

    #endregion

}

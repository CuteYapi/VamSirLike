using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStatus Status;
    public PlayerSkill Skill;

    public int CurrentHp { get; private set; }

    private float mTimer;
    private float mAttackInterval = 1f;


    public float HpRatio
    {
        get => (float)CurrentHp / Status.MaxHp;
    }

    public bool IsDead
    {
        get => CurrentHp <= 0;
    }

    private void Update()
    {
        PlayerMoveInput();

        mTimer += Time.deltaTime;

        if (mTimer >= mAttackInterval)
        {
            Skill.AttackNearPlayerSquare();
            mTimer = 0;
        }
    }

    private void PlayerMoveInput()
    {
        // Ű �Է�
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            transform.position += ( Vector3.up * Time.deltaTime ) * Status.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            transform.position += Vector3.down * Time.deltaTime * Status.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            transform.position += Vector3.left * Time.deltaTime * Status.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            transform.position += Vector3.right * Time.deltaTime * Status.MoveSpeed;
        }
    }

    public void Initialize()
    {
        Status.TempStatusInit();
        SetPosition(Vector3.zero);
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

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
}

using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private Dictionary<HpReason, int> mHpReasonDictionary = new Dictionary<HpReason, int>();
    private int mMaxHp;
    public int MaxHp
    {
        get
        {
            mMaxHp = 0;
            foreach (var hpValue in mHpReasonDictionary)
            {
                mMaxHp += hpValue.Value;
            }

            return mMaxHp;
        }
    }

    private Dictionary<AtkReason, int> mAtkReasonDictionary = new Dictionary<AtkReason, int>();
    private int mAtk;
    public int Atk
    {
        get
        {
            mAtk = 0;
            foreach (var atkValue in mAtkReasonDictionary)
            {
                mAtk += atkValue.Value;
            }

            return mAtk;
        }
    }

    private Dictionary<MoveSpeedReason, int> mMoveSpeedReasonDictionary = new Dictionary<MoveSpeedReason, int>();
    private int mMoveSpeed;
    public int MoveSpeed
    {
        get
        {
            mMoveSpeed = 0;
            foreach (var atkValue in mMoveSpeedReasonDictionary)
            {
                mMoveSpeed += atkValue.Value;
            }

            return mMoveSpeed;
        }
    }

    public void AddHpReason(HpReason reason, int value)
    {
        if (mHpReasonDictionary.TryAdd(reason, value) == false)
        {
            // Dic에 넣는 것에 실패했다 => 이미 해당 Key 값이 존재
            mHpReasonDictionary[reason] = value;

        }

#if Log
        Log.Message(LogType.StatHp, $"MaxHp 수치 변화 {value}");
#endif
    }
    public void RemoveHpReason(HpReason reason)
    {
        if (mHpReasonDictionary.Remove(reason) == false)
        {
            Log.Error(LogType.StatHp, $"해당 {reason}가 없습니다!");
        }
    }

    public void AddAtkReason(AtkReason reason, int value)
    {
        if (mAtkReasonDictionary.TryAdd(reason, value) == false)
        {
            // Dic에 넣는 것에 실패했다 => 이미 해당 Key 값이 존재
            mAtkReasonDictionary[reason] = value;

        }

#if Log
        Log.Message(LogType.StatAtk, $"공격력 수치 변화 {value}");
#endif
    }

    public void RemoveAtkReason(AtkReason reason)
    {
        if (mAtkReasonDictionary.Remove(reason) == false)
        {
            Log.Error(LogType.StatAtk, $"해당 {reason}가 없습니다!");
        }
    }

    public void AddMoveSpeedReason(MoveSpeedReason reason, int value)
    {
        if (mMoveSpeedReasonDictionary.TryAdd(reason, value) == false)
        {
            // Dic에 넣는 것에 실패했다 => 이미 해당 Key 값이 존재
            mMoveSpeedReasonDictionary[reason] = value;

        }

#if Log
        Log.Message(LogType.StatMoveSpeed, $"이동속도 수치 변화 {value}");
#endif
    }
    public void RemoveMoveSpeedReason(MoveSpeedReason reason)
    {
        if (mMoveSpeedReasonDictionary.Remove(reason) == false)
        {
            Log.Error(LogType.StatMoveSpeed, $"해당 {reason}가 없습니다!");
        }
    }


    #region Editor

    public void TempStatusInit()
    {
        AddHpReason(HpReason.Level, 10);
        AddAtkReason(AtkReason.Level, 1);
        AddMoveSpeedReason(MoveSpeedReason.Level, 1);
    }

    #endregion
}

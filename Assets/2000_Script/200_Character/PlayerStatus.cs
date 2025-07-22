using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public Dictionary<HpReason, int> HpReasonDictionary = new Dictionary<HpReason, int>();
    private int mHp;

    public int Hp
    {
        get
        {
            mHp = 0;
            foreach (var hpValue in HpReasonDictionary)
            {
                mHp += hpValue.Value;
            }

            return mHp;

        }
    }


    public Dictionary<AtkReason, int> AtkReasonDictionary = new Dictionary<AtkReason, int>();
    private int mAtk;
    public int Atk
    {
        get
        {
            mAtk = 0;
            foreach (var atkValue in AtkReasonDictionary)
            {
                mAtk += atkValue.Value;
            }

            return mAtk;
        }
    }

    public Dictionary<MoveSpeedReason, int> MoveSpeedReasonDictionary = new Dictionary<MoveSpeedReason, int>();
    private int mMoveSpeed;
    public int MoveSpeed
    {
        get
        {
            mMoveSpeed = 0;
            foreach (var moveSpeedValue in MoveSpeedReasonDictionary)
            {
                mMoveSpeed += moveSpeedValue.Value;
            }

            return mMoveSpeed;
        }
    }


    public void AddHpReason(HpReason reason, int value)
    {
        if (HpReasonDictionary.TryAdd(reason, value) == false)
        {
            // Dic에 넣는 것에 실패했다 => 이미 해당 Key 값이 존재
            HpReasonDictionary[reason] = value;

        }

#if Log
        Log.Message(LogType.StatHp, $"Hp 수치 변화 {value}");
#endif
    }
    public void RemoveHpReason(HpReason reason)
    {
        if (HpReasonDictionary.Remove(reason) == false)
        {
            Log.Error(LogType.StatHp, $"해당 {reason}가 없습니다!");
        }
    }

    public void AddAtkReason(AtkReason reason, int value)
    {
        if (AtkReasonDictionary.TryAdd(reason, value) == false)
        {
            // Dic에 넣는 것에 실패했다 => 이미 해당 Key 값이 존재
            AtkReasonDictionary[reason] = value;

        }

#if Log
        Log.Message(LogType.StatAtk, $"공격력 수치 변화 {value}");
#endif
    }

    public void RemoveAtkReason(AtkReason reason)
    {
        if (AtkReasonDictionary.Remove(reason) == false)
        {
            Log.Error(LogType.StatAtk, $"해당 {reason}가 없습니다!");
        }
    }

    public void AddMoveSpeedReason(MoveSpeedReason reason, int value)
    {
        if (MoveSpeedReasonDictionary.TryAdd(reason, value) == false)
        {
            // Dic에 넣는 것에 실패했다 => 이미 해당 Key 값이 존재
            MoveSpeedReasonDictionary[reason] = value;

        }

#if Log
        Log.Message(LogType.StatMoveSpeed, $"이동속도 수치 변화 {value}");
#endif
    }
    public void RemoveMoveSpeedReason(MoveSpeedReason reason)
    {
        if (MoveSpeedReasonDictionary.Remove(reason) == false)
        {
            Log.Error(LogType.StatMoveSpeed, $"해당 {reason}가 없습니다!");
        }
    }
}

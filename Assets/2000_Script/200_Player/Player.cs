using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Hp { get; private set; }


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
            foreach (var atkValue in MoveSpeedReasonDictionary)
            {
                mMoveSpeed += atkValue.Value;
            }

            return mMoveSpeed;
        }
    }

    private void Update()
    {
        PlayerMoveInput();
    }

    private void PlayerMoveInput()
    {
        // 키 입력
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            transform.position += (Vector3.up * Time.deltaTime) * MoveSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            transform.position += Vector3.down * Time.deltaTime * MoveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            transform.position += Vector3.left * Time.deltaTime * MoveSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
        }
    }

    [Button("Add Atk")]
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

    [Button("Add MoveSpeed")]
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
}

#if Log

using System;
using UnityEngine;

public enum LogType
{
    None = -1,

    AllLog = 0,

    StatHp,
    StatAtk,
    StatMoveSpeed,

    UI_WorldToScreen,
    
    Max,
}


public static class Log 
{
    public static void Message(LogType type, string message)
    {
        if (type == LogType.None || type == LogType.Max)
        {
            Debug.Log($"잘못된 로그 타입이 입력되었습니다: {type}");
            return;
        }

        if (Manager.Log.LogOn[(int)LogType.AllLog] == false)
        {
            return;
        }

        if (Manager.Log.LogOn[(int)type] == false)
        {
            return;
        }

        string typeMessage = String.Empty;

        switch (type)
        {
            case LogType.StatAtk:
            {
                typeMessage = $"<Color=red>[{type}]</color> ";
            }
                break;
            case LogType.StatMoveSpeed:
            {
                typeMessage = $"<Color=green>[{type}]</color> ";
            }
                break;
            case LogType.StatHp:
            {
                typeMessage = $"<Color=blue>[{type}]</color> ";
            }
                break;
        }

        Debug.Log(typeMessage + message);
    }

    public static void Error(LogType type, string message)
    {
        string typeMessage = String.Empty;
        switch (type)
        {
            case LogType.StatAtk:
            {
                typeMessage = $"<Color=red>[{type}]</color> ";
            }
                break;
            case LogType.StatMoveSpeed:
            {
                typeMessage = $"<Color=green>[{type}]</color> ";
            }
                break;
        }

        Debug.LogError(typeMessage + message);
    }
}

#endif

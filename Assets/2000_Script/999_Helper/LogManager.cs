using UnityEngine;

public class LogManager : MonoBehaviour
{
    public bool[] LogOn;

    private void Awake()
    {
        LogOn = new bool[(int)LogType.Max];

        for (int j = 0; j < (int)LogType.Max; j++)
        {
            LogOn[j] = false;
        }
    }
}

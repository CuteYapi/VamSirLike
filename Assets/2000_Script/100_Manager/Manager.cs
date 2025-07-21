using UnityEngine;

public class Manager : MonoBehaviour
{
    void Start()
    {
        SetLogManager();
    }

    public static LogManager Log { get; private set; }

    public void SetLogManager()
    {
        Log = transform.GetComponentInChildren<LogManager>();
    }
}

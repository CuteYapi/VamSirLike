using UnityEngine;

public class Manager : MonoBehaviour
{
    private void Start()
    {
        Initialize();
    }
    
    void Initialize()
    {
        SetLogManager();
        SetDataManager();
        SetStageManager();
        SetPoolManager();
    }

    public static LogManager Log { get; private set; }
    public void SetLogManager()
    {
        Log = transform.GetComponentInChildren<LogManager>();
    }

    public static DataManager Data { get; private set; }
    public void SetDataManager()
    {
        Data = transform.GetComponentInChildren<DataManager>();
        Data.Initialize();
    }

    public static StageManager Stage { get; private set; }

    public void SetStageManager()
    {
        Stage = transform.GetComponentInChildren<StageManager>();
        Stage.Initialize();
    }

    public static PoolManager Pool { get; private set; }

    public void SetPoolManager()
    {
        Pool = transform.GetComponentInChildren<PoolManager>();
    }
}

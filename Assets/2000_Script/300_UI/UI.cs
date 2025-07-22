using UnityEngine;

public class UI : MonoBehaviour, IUI
{
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        SetDebugController();

        Open();
    }

    public void Open()
    {

    }

    public void Close()
    {

    }

    public static DebugController Debug { get; private set; }

    private void SetDebugController()
    {
        Debug = transform.GetComponentInChildren<DebugController>();
        Debug.Initialize();
    }
}

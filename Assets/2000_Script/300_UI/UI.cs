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
        SetWorldToScreenController();

        Open();
    }

    public void Open()
    {
        WorldToScreen.Open();
    }

    public void Close()
    {
        WorldToScreen.Close();
    }

    public static DebugController Debug { get; private set; }

    private void SetDebugController()
    {
        Debug = transform.GetComponentInChildren<DebugController>();
        Debug.Initialize();
    }

    public static WorldToScreenController WorldToScreen { get; private set; }

    private void SetWorldToScreenController()
    {
        WorldToScreen = transform.GetComponentInChildren<WorldToScreenController>();
        WorldToScreen.Initialize();
    }
}

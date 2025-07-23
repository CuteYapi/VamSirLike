using UnityEngine;

public partial class DataManager : MonoBehaviour
{
    public Player Player;

    public float SpawnDistance;

    public void Initialize()
    {
        Player = Instantiate(RefPlayer);
        Player.Initialize();
    }
}

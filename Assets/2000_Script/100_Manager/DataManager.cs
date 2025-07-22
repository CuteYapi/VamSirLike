using UnityEngine;

public partial class DataManager : MonoBehaviour
{
    public Player Player;

    public void Initialize()
    {
        Player = Instantiate(RefPlayer);
    }
}

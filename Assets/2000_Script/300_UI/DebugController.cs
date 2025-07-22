using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebugController : MonoBehaviour, IUI
{
    public List<Button> ButtonList = new List<Button>();

    public void Initialize()
    {
        ButtonList[0].onClick.AddListener(() =>
        {

        });
        ButtonList[1].onClick.AddListener(() =>
        {

        });
        ButtonList[2].onClick.AddListener(() =>
        {

        });
        ButtonList[3].onClick.AddListener(() =>
        {
            MoveSpeedReasonButton();
        }); 
        ButtonList[4].onClick.AddListener(() =>
        {
            Manager.Stage.SpawnMonster();
        });
        ButtonList[5].onClick.AddListener(() =>
        {
            RemoveAllEnemy();
        });
    }

    public void Open()
    {

    }

    public void Close()
    {

    }

    public void AddHpReasonButton()
    {
        Manager.Data.Player.Status.AddAtkReason(AtkReason.Level, 10);
    }

    public void MoveSpeedReasonButton()
    {
        Manager.Data.Player.Status.AddMoveSpeedReason(MoveSpeedReason.Level, 10);
    }

    public void RemoveAllEnemy()
    {
        foreach (var enemy in Manager.Stage.EnemyList)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}

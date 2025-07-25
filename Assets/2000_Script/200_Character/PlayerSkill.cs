using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public Player Player;

    public Animator Animator;

    public void AttackNearPlayerSquare()
    {
        Animator.Play("SlashFx");

        SkillAreaInfo skillAreaData = new SkillAreaInfo();
        skillAreaData.TargetPosition = transform.position;
        skillAreaData.Size = new Vector3(3, 3, 0);
        skillAreaData.Rotation = Vector3.zero;

        List<Enemy> targetEnemyList = GetEnemyInBox(skillAreaData);

        foreach (Enemy enemy in targetEnemyList)
        {
            var resultDamage = Random.Range(Player.Status.Atk - 1, Player.Status.Atk + 2);
            enemy.GetDamaged(resultDamage);
        }
    }

    private List<Enemy> GetEnemyInBox(SkillAreaInfo skillAreaInfo)
    {
        Vector3 targetPosition = skillAreaInfo.TargetPosition;
        Vector3 targetBoxSize = skillAreaInfo.Size;
        Collider2D[] results = new Collider2D[10];
        Quaternion searchRotation = Quaternion.Euler(skillAreaInfo.Rotation);

        results = Physics2D.OverlapBoxAll(targetPosition, targetBoxSize, 0f);

        List<Enemy> targetEnemyList = new List<Enemy>();
        foreach (var collider2D in results)
        {
            if (collider2D == null)
            {
                break;
            }

            Enemy targetEnemy = collider2D.GetComponent<Enemy>();
            if (targetEnemy == null)
            {
                continue;
            }

            targetEnemyList.Add(targetEnemy);
        }

        return targetEnemyList;
    }
}

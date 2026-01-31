using UnityEngine;

public interface EnemyAttack
{
   public void InitializeSkill(float damage, Vector2 direction, Vector3 basePosition);
   public void Shoot();
}

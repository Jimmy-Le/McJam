using UnityEngine;

public class Health : MonoBehaviour
{

    public int maxHealth = 3;
    public int currentHealth;
    
    public Animator animator;
    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakingDamage(int damage)
    {
        currentHealth -= damage;
        // Play damage sound

        if (currentHealth <= 0)
        {
            //Replace red heart with grey heart
            //Death animation
            // anim.setBool("isDead", true);
            //Initiate Game Over Screen
        }
    }
}

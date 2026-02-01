using UnityEngine;
using System.Collections;

public class DragonScript : MonoBehaviour, Enemy
{
    [SerializeField] public float health;
    [SerializeField] public int attack = 1;
    

    [SerializeField] public Vector2 direction;

	[SerializeField] private GameObject diceAttack;

	private float attackTimer = 0;
	private float attackTime = 5f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 150f;
    }

    // Update is called once per frame
    void Update()
    {
		if(attackTimer > attackTime)
		{
			
			diceAttack.GetComponent<DiceAttack>().StartAttack();
			attackTimer = 0f;
		} 
		else 
		{
			attackTimer += Time.deltaTime;
		}
        
    }
    
    

    public void TakeDamage(float damage)
    {
        health -= damage;
        DragonHealth.DragonHP.UpdateDragonHealth(health);
        if (health <= 0)
        {
            KilledBoss();
        }
    }

    public void KilledBoss()
    {
        Destroy(this.gameObject);
    }

    public void SetAttack()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(1);
        }
    }


}

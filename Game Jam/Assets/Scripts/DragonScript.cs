using UnityEngine;
using System.Collections;

public class DragonScript : MonoBehaviour, Enemy
{
    [SerializeField] public float health;
    [SerializeField] public int attack = 1;
    [SerializeField] private GameManager gameManager;

    [SerializeField] public Vector2 direction;

	[SerializeField] private GameObject diceAttack;

	private float attackTimer = 0;
	private float attackTime = 5f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 250f;
        MusicManager.Instance.PlayMusic("Boss-Dragon");

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
        SoundManager.Instance.PlaySound2D("Boss-Hurt", 1f, 0.1f);
        health -= damage;
        DragonHealth.DragonHP.UpdateDragonHealth(health);
        if (health <= 0)
        {
            KilledBoss();
        }
    }

    public void KilledBoss()
    {
		gameManager.BossKilled();
        Destroy(this.gameObject);
        SoundManager.Instance.PlaySound2D("Boss-Dead", 1f, 0.1f);


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

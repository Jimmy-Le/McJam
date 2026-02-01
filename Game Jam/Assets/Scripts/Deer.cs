using UnityEngine;

public class Deer : MonoBehaviour, Enemy
{
    [SerializeField] public float health;
    [SerializeField] public int attack = 1;
    

    [SerializeField] public Vector2 direction;
	[SerializeField] private GameObject powerUp;
	[SerializeField] private GameManager gameManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            KilledBoss();
        }
    }

    public void KilledBoss()
    {
		gameManager.BossKilled();
		Instantiate(powerUp, transform.position, Quaternion.identity);
	
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

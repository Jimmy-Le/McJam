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
        health = 150f;
        MusicManager.Instance.PlayMusic("Boss-Deer");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        SoundManager.Instance.PlaySound2D("Deer-hurt", 1f, 0.1f);


        health -= damage;
        DeerHealth.DeerHP.UpdateDeerHealth(health);
        if (health <= 0)
        {
            KilledBoss();
            SoundManager.Instance.PlaySound2D("Deer-die", 0.1f, 0.1f);
        }
    }

    public void KilledBoss()
    {
		gameManager.BossKilled();
		Instantiate(powerUp, transform.position, Quaternion.identity);
        MusicManager.Instance.StopMusic();
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

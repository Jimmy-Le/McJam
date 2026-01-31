using UnityEngine;

public class Deer : MonoBehaviour, Enemy
{
    [SerializeField] public float health;
    [SerializeField] public int attack = 1;
    [SerializeField] public float speed = 2f;

    [SerializeField] public Vector2 direction;
    
    
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
        Destroy(this.gameObject);
    }

    public void SetAttack()
    {
        
    }
}

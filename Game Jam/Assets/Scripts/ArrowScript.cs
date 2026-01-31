using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public float damage;
    public Vector2 direction = new Vector2(1, 0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Since we are hardcoding the direction to be one or the other (x or y), we can prob do it in one line
        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0, Space.World);

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hi");
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }

    public void InitializeArrow(float playerDamage, Vector2 playerDirection)
    {
        direction = playerDirection;
        damage = playerDamage;
    }
    
    
}

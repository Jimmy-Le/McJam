using UnityEngine;

public class ArrowScript : MonoBehaviour, MainAttack
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 3f;
    public float damage;
    public Vector2 direction = new Vector2(1, 0);
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // Since we are hardcoding the direction to be one or the other (x or y), we can prob do it in one line
        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0, Space.World);

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
       
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }

    public void InitializeArrow(float playerDamage, Vector2 playerDirection, float speed)
    {
        direction = playerDirection;
        damage = playerDamage;
        this.speed = speed;
        
        // Compute rotation from direction (right-facing base sprite)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }
    
    
    
}

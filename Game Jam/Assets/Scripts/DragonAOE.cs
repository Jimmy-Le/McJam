using UnityEngine;

public class DragonAOE : MonoBehaviour
{
    
    private Vector3 snapshotTarget;
    private float timer = 0;
    private float duration = 3f;
    
    // Launch a projectile towards the player's position on cast
    
    void Update() {
        timer += Time.deltaTime;
        if(timer >= duration)
        {
            Destroy(this.gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(1);
            other.gameObject.GetComponent<PlayerMovement>().GetSlowed();
         
        }
        
    }
}

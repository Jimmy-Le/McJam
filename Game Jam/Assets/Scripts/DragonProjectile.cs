using UnityEngine;

public class DragonProjectile : MonoBehaviour
{

    [SerializeField] private float speed = 7f;
    private Vector3 snapshotTarget;
    private bool launched = false;
    
    // Launch a projectile towards the player's position on cast
    public void Launch(Vector3 playerSnapshotPos) {
        snapshotTarget = playerSnapshotPos;  
        launched = true;
        SoundManager.Instance.PlaySound2D("Boss-Fire", -0.7f);

    }
    
    void Update() {
        if (!launched) return;



        transform.position = Vector3.MoveTowards(transform.position, snapshotTarget, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, snapshotTarget) < 0.1f) {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(1);
            Destroy(gameObject);
        }
        
    }
}

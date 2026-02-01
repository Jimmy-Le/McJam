using UnityEngine;

public class BowScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Vector2 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.direction;
        
        // Compute rotation from direction (right-facing base sprite)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }

}

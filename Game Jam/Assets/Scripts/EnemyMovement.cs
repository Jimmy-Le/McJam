using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    // States
    private bool isWalking = true;
    
    // Stats
    [SerializeField] public float speed = 2f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get player object by tag 
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {

            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

            
        }
        
        
    }
}

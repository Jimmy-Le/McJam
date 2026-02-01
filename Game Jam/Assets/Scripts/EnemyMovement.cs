using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;

    [SerializeField] private GameObject attackIndicator;
	[SerializeField] private Animator animator; 
    // States
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool isAntlerDash = false;
    
    // Stats
    
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float minDistance = 4f;
    
    // Boss Attack Timer
    private float timer = 0;
    private float attackTime = 5f;

    private Vector3 currentTargetPosition;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get player object by tag 
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWalking && !isAntlerDash && !isAttacking)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
                    if (distance < minDistance)
                    {
                        isWalking = true;
						animator.SetBool("isWalking", true);
                    }
        }
        
        
        
        if (isWalking)
        {
            Walk();
            isAttacking = true;
        }

        if (isAttacking && !isAntlerDash)
        {
            timer += Time.deltaTime;
            if (timer >= attackTime)
            {
                StartCoroutine(AntlerAttack());
                timer = 0;
            }
        }
        
        
        
    }

    private IEnumerator AntlerAttack()
    {   
        isWalking = false;
        isAttacking = false;
		animator.SetBool("isWalking", false);
        animator.SetBool("isAntlerDashing", true);
        IncreaseSpeed();
        yield return new WaitForSeconds(1.5f);
        
        currentTargetPosition = playerTransform.position;
        Instantiate(attackIndicator, currentTargetPosition, Quaternion.Euler(0, 0, 0));
        Debug.Log(currentTargetPosition);
        
        while (Vector3.Distance(transform.position, currentTargetPosition) > 0.3f)
        {
            // Debug.Log("inside loop" + currentTargetPosition);
            transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Debug.Log("out of loop");
        ResetSpeed();
        
        
        // yield return new WaitForSeconds(1.5f);
        isAntlerDash = false;
        isWalking = true;
		animator.SetBool("isAntlerDashing", false);
		animator.SetBool("isWalking", true);
        
        
    }

    private void Walk()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            
        Vector3 direction = (playerTransform.position - transform.position).normalized;
            
        if (direction.x > 0.1f) {
            // Moving right
            spriteRenderer.flipX = true;
        } else if (direction.x < -0.1f) {
            // Moving left
            spriteRenderer.flipX = false;
        }
    }

    public void IncreaseSpeed()
    {
        speed = speed * 2;
    }

    public void ResetSpeed()
    {
        speed = baseSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            isWalking = true;
			animator.SetBool("isWalking", true);
        }
        
    }
}

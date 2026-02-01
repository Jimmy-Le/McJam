using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    // Keybinds
    [SerializeField] 
    public InputActionAsset inputAction;
    private InputAction move_action;
    private InputAction attack_action;
    private InputAction dodge_action;
    private InputAction skill_action;

	// Animation
	[SerializeField] private Animator animator; 
	[SerializeField] private Animator bowAnimator; 
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Rigidbody2D rb;

    
    // Stats
	[SerializeField]
	public int health = 3;
	
	[SerializeField]
	public float attack = 5f;
    [SerializeField] 
    public float speed = 5f;

	[SerializeField] 
    public float baseSpeed = 5f;
	[SerializeField]
	public float attackSpeed = 3f;
    [SerializeField] 
    public Vector2 direction = new Vector2(1,0);
	
    // Death
	[SerializeField] 
	private GameManager gameManager;
    private bool isDead = false;
	

	// Invincibility
	public bool isInvincible = false;
	public float invincibleTimer = 0f;
	public float invincibleDuration = 0.1f;

	// Attack / Skill Prefabs
	[SerializeField]
	public GameObject mainAttack;
	[SerializeField]
	public GameObject skill;

	// Main Attack Timer
	public float mainAttackTimer = 0;
	private float mainAttackCooldown = 0.5f;
	
	// Special Attack 
	public float skillTimer = 0;
	private float skillCooldown = 5f;

	// Dodge
	public bool isDodging = false;
	public float dodgeTimer = 0;
	public float dodgeDurationTimer = 0f;
	public float dodgeDuration = 2f;
	private float dodgeCooldown = 2.5f;
	private float dodgeSpeed = 12f;
	public float dodgeDistance = 1f;
	private Vector3 targetPosition;
	private Vector3 currentPosition;


    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        move_action = inputAction.FindAction("Move");
        attack_action = inputAction.FindAction("Attack");
        dodge_action = inputAction.FindAction("Sprint");
        skill_action = inputAction.FindAction("Skill");
        
    }
    

    // Update is called once per frame
    void Update()
    {
	    if(isDead)
	    {
		    return;
	    }
		// Timers
		if(mainAttackTimer > 0)
		{
			mainAttackTimer -= Time.deltaTime;
		}
		if(skillTimer > 0)
		{
			skillTimer -= Time.deltaTime;
		}
		if(dodgeTimer > 0)
		{
			dodgeTimer -= Time.deltaTime;
		}
		if(dodgeDurationTimer > 0 && isDodging){
			dodgeDurationTimer -= Time.deltaTime;
		} else {
			dodgeDurationTimer = dodgeDuration;
			isDodging = false;
		}

		if(invincibleTimer > 0)
		{
			
			invincibleTimer -= Time.deltaTime;
		} else {
			spriteRenderer.color = Color.white;  // R>1 overbright
			isInvincible = false;

		}
		
		
		if (attack_action.IsPressed() )
		{
			if(mainAttackTimer <= 0)
			{
				Attack();
				mainAttackTimer = mainAttackCooldown;
			} 
		}

		if (skill_action.IsPressed() )
		{
			if(skillTimer <= 0)
			{
				Skill();
				skillTimer = skillCooldown;
			} 
			
		}

		if (dodge_action.IsPressed() && !isDodging )
		{
			if(dodgeTimer <= 0)
			{
				isDodging = true;
				isInvincible = true;
				currentPosition = transform.position;
				targetPosition = new Vector3(currentPosition.x + (dodgeDistance * direction.x), currentPosition.y + (dodgeDistance * direction.y), currentPosition.z);
				spriteRenderer.color = new Color(1.5f, 10f, 2f, 1f);  // R>1 overbright
				dodgeTimer = dodgeCooldown;
			}
		}

		if(isDodging)
		{
			Dodge();
			
		}

      
    }

	void FixedUpdate(){
	    if(isDead)
        {
            return;
        }

		if (move_action.IsPressed() && !isDodging)
        {
			
            Movement();

        }
	}

	private void Movement(){

		
		Vector2 movement = move_action.ReadValue<Vector2>();
		// Move diagonal
        if(movement.x != 0 && movement.y != 0)
		{
			direction = new Vector2(movement.x, 0);
		
			
		} else if(movement.y != 0 && movement.x == 0) 
		{
			direction = new Vector2(0, movement.y);
		} else if(movement.x != 0 && movement.y == 0)
		{
			direction = new Vector2(movement.x, 0);
		}

		// if moving left
		if(direction.x < 0){
			spriteRenderer.flipX = true;
			animator.SetBool("isFront", false);
			animator.SetBool("isBack", false);
		} else if (direction.x > 0)	// Moving right
		{
			spriteRenderer.flipX = false;
			animator.SetBool("isFront", false);
			animator.SetBool("isBack", false);
		} else if (direction.y < 0) // Moving down ?
		{
			spriteRenderer.flipX = false;
			animator.SetBool("isFront", true);
			animator.SetBool("isBack", false);
		
		} else if (direction.y > 0)
		{
			spriteRenderer.flipX = false;
			animator.SetBool("isFront", false);
			animator.SetBool("isBack", true);
		} else 
		{
			spriteRenderer.flipX = false;
			animator.SetBool("isFront", false);
			animator.SetBool("isBack", false);
		}


        transform.Translate(0, movement.y * speed * Time.deltaTime, 0, Space.World);
        transform.Translate(movement.x * speed * Time.deltaTime,0, 0, Space.World);

	}

	private void Attack()
	{

		bowAnimator.SetTrigger("isAttacking");
		GameObject arrow = Instantiate(mainAttack, transform.position, Quaternion.Euler(0,0,0));
		arrow.GetComponent<MainAttack>().InitializeArrow(attack, direction, attackSpeed);

	}

	private void Skill()
	{
		bowAnimator.SetTrigger("isAttacking");
		skill.GetComponent<SkillAttack>().InitializeSkill(attack, direction, transform.position);
		GameObject skillObject = Instantiate(skill, transform.position, Quaternion.Euler(0,0,0));
		skillObject.GetComponent<SkillAttack>().Shoot();

	}

	private void Dodge()
	{
		isInvincible = true;
		invincibleTimer = invincibleDuration;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, dodgeSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) {
            isDodging = false; 
		
        }
		
		
	}
	
	
	

	public void TakeDamage(int damage){
		if(!isInvincible && !isDead)
		{
			animator.SetTrigger("isHit");
			health -= damage;
			if (health <= 0){
				Die();
			} else {
				isInvincible = true;
				invincibleTimer = invincibleDuration;
				spriteRenderer.color = new Color(2f, 1.5f, 2f, 1f);  // R>1 overbright
			}
		}
	}

	public void Die()
	{
		rb.bodyType = RigidbodyType2D.Kinematic;
		animator.SetBool("isDead", true);
		isDead = true;
		gameManager.GameOver();	
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy"))
		{
			targetPosition = transform.position;
		}
	}

	public void GetSlowed()
	{
		StartCoroutine(Slowed());
	}

	public IEnumerator Slowed()
	{
		speed = speed/2f;
		yield return new WaitForSeconds(3f);
		speed = baseSpeed;
		yield return null;
		
	}

}

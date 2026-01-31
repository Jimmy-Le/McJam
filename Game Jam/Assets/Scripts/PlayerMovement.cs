using UnityEngine;
using UnityEngine.InputSystem;


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
	[SerializeField] private SpriteRenderer spriteRenderer;

    
    // Stats
	[SerializeField]
	public int health = 3;
	
	[SerializeField]
	public float attack = 5f;
    [SerializeField] 
    public float speed = 2f;
	[SerializeField]
	public float attackSpeed = 3f;
    [SerializeField] 
    public Vector2 direction = new Vector2(1,0);

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
	private float skillCooldown = 2f;

	// Dodge
	public bool isDodging = false;
	public float dodgeTimer = 0;
	public float dodgeDurationTimer = 0f;
	public float dodgeDuration = 2f;
	private float dodgeCooldown = 1f;
	public float dodgeSpeed = 10f;
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
		

		
		if (attack_action.IsPressed())
		{
			if(mainAttackTimer <= 0)
			{
				Attack();
				mainAttackTimer = mainAttackCooldown;
			} 
		}

		if (skill_action.IsPressed())
		{
			if(skillTimer <= 0)
			{
				Skill();
				skillTimer = skillCooldown;
			} 
			
		}

		if (dodge_action.IsPressed() && !isDodging)
		{
			if(dodgeTimer <= 0)
			{
				isDodging = true;
				currentPosition = transform.position;
				targetPosition = new Vector3(currentPosition.x + (dodgeDistance * direction.x), currentPosition.y + (dodgeDistance * direction.y), currentPosition.z);
				dodgeTimer = dodgeCooldown;
			}
		}

		if(isDodging)
		{
			Dodge();
			
		}

      
    }

	void FixedUpdate(){
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

		mainAttack.GetComponent<MainAttack>().InitializeArrow(attack, direction, attackSpeed);
		Instantiate(mainAttack, transform.position, Quaternion.Euler(0,0,0));

	}

	private void Skill()
	{

		skill.GetComponent<SkillAttack>().InitializeSkill(attack, direction, transform.position);
		GameObject skillObject = Instantiate(skill, transform.position, Quaternion.Euler(0,0,0));
		skillObject.GetComponent<SkillAttack>().Shoot();

	}

	private void Dodge()
	{
		
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, dodgeSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) {
            isDodging = false; 
        }
		
		
	}
	
	
	

	public void TakeDamage(int damage){
		health -= damage;
		if (health <= 0){
			Debug.Log("Game Over");
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy"))
		{
			targetPosition = transform.position;
		}
	}

}

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
    
    // Stats
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
	private float mainAttackTimer = 0;
	private float mainAttackCooldown = 0.5f;
	
	// Special Attack 
	private float skillTimer = 0;
	private float skillCooldown = 2f;

    
    
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
		
		if (attack_action.IsPressed())
		{
			if(mainAttackTimer > 0)
			{
				mainAttackTimer -= Time.deltaTime;
			} else 
			{
				Attack();
				mainAttackTimer = mainAttackCooldown;
			} 
			
		}

		if (skill_action.IsPressed())
		{
			if(skillTimer > 0)
			{
				skillTimer -= Time.deltaTime;
			} else 
			{
				Skill();
				skillTimer = skillCooldown;
			} 
			
		}

      
    }

	void FixedUpdate(){
		if (move_action.IsPressed())
        {
            Movement();

        }
	}

	private void Movement(){
		Vector2 movement = move_action.ReadValue<Vector2>();
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

}

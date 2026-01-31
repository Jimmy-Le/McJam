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
    private float speed = 2f;

    [SerializeField] 
    public float upDirection;

    [SerializeField] 
    public float sideDirection;
    
    
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
        if (move_action.IsPressed())
        {
            Vector2 movement = move_action.ReadValue<Vector2>();
            upDirection = movement.y;
            sideDirection = movement.x;



            transform.Translate(0, upDirection * speed * Time.deltaTime, 0, Space.World);
            transform.Translate(sideDirection * speed * Time.deltaTime,0, 0, Space.World);
        }
        
    }
}

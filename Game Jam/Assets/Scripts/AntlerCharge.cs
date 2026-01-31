using UnityEngine;
using System.Collections;

public class AntlerCharge : MonoBehaviour, EnemyAttack
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public GameObject skillObject;
    public float speed = 5f;
    public float damage;
 
    public Vector2 direction = new Vector2(1, 0);
    
    public Vector3 arrowPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    

    public void InitializeSkill(float playerDamage, Vector2 playerDirection, Vector3 basePosition)
    {

    }


    public void Shoot()
    {

    }


}

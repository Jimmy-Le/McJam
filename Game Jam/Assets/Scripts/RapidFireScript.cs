using UnityEngine;
using System.Collections;

public class RapidFireScript : MonoBehaviour, SkillAttack
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public GameObject arrow;
    public float speed = 5f;
    public float damage;
    
    public float timeBetweenShots = 0.5f;
    public Vector2 direction = new Vector2(1, 0);
    
    public Vector3 arrowPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    

    public void InitializeSkill(float playerDamage, Vector2 playerDirection, Vector3 basePosition)
    {
        direction = playerDirection;
        damage = playerDamage * 2;
        arrowPosition = basePosition;
       
        
        
        arrow.gameObject.GetComponent<MainAttack>().InitializeArrow(damage, direction, speed);

        

    }

    private IEnumerator ShootArrows(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(arrow, arrowPosition, Quaternion.Euler(0,0,0));
            yield return new WaitForSeconds(timeBetweenShots);
        }
        Destroy(this.gameObject);
        
    }

    public void Shoot()
    {
        StartCoroutine(ShootArrows(3));
        
    }


}

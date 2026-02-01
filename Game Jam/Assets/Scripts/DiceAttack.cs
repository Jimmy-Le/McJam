using UnityEngine;
using System.Collections;

public class DiceAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject upperBound;
    [SerializeField] private GameObject lowerBound;
    
    [SerializeField] private Transform playerTransform;
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject aoePrefab;
    [SerializeField] private GameObject attackIndicator;

    [SerializeField] private Sprite[] dices;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int choice;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAttack()
    {
        StartCoroutine(RollDice());
    }
    
    public IEnumerator RollDice()
    {
        int roll = Random.Range(1, 7);
        
        // Change sprite to 3
        spriteRenderer.sprite = dices[2];
        // Wait 1 second
        yield return new WaitForSeconds(1f);
        // Change sprite to 2
        spriteRenderer.sprite = dices[1];
        // Wait 1 second
        yield return new WaitForSeconds(1f);
        // Change sprite to 1
        spriteRenderer.sprite = dices[0];
        // Wait 1 Second 
        yield return new WaitForSeconds(1f);
        // Flash Dice
        // Change sprite to the "roll" number
        spriteRenderer.sprite = dices[roll - 1];

        Debug.Log($"Rolled a {roll}");
        if (roll == 6)
        {
            // Do 10 DMG attack to the whole board
            Vector3 snapshot = playerTransform.position;
            Instantiate(attackIndicator, snapshot, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(2f);
            ShootAoe(snapshot);
        }
        else
        {
            // Do # of attacks based on roll (5 = 5 projectiles)
            for (int i = 0; i < roll; i++)
            {
                // Randomize Coordinates
                // Instantiate attacks
                // Initialize Attacks to go towards the position
                // wait 0.5 seconds
                Shoot();
                yield return new WaitForSeconds(0.5f);

            }
        }

        yield return null;
        
    }
    
    void Shoot() {
        // SNAPSHOT at launch time
        Vector3 snapshot = playerTransform.position;
        ShootProjectile(snapshot);


    }

    private void ShootProjectile(Vector3 snapshot)
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        DragonProjectile script = proj.GetComponent<DragonProjectile>();
        Instantiate(attackIndicator, snapshot, Quaternion.Euler(0, 0, 0));
        script.Launch(snapshot);  
    }

    private void ShootAoe(Vector3 snapshot)
    {
        
        GameObject proj = Instantiate(aoePrefab,snapshot, Quaternion.identity);
    }
}

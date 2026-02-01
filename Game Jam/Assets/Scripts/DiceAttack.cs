using UnityEngine;
using System.Collections;

public class DiceAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator RollDice()
    {
        int roll = Random.Range(1, 7);
        
        // Change sprite to 3
        // Wait 1 second
        // Change sprite to 2
        // Wait 1 second
        // Change sprite to 1
        // Wait 1 Second 
        // Flash Dice
        // Change sprite to the "roll" number

        if (roll == 6)
        {
            // Do 10 DMG attack to the whole board
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
            }
        }

        yield return null;
        
    }
}

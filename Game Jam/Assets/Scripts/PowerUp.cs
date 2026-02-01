using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound2D("Player-Pick", 0.5f);


            Debug.Log("Hi");
            other.gameObject.GetComponent<PlayerMovement>().UnlockPowerUp();
            Destroy(this.gameObject);
        }
    }
}

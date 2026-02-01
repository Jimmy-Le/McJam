using UnityEngine;

public class TargetScript : MonoBehaviour
{

	private float timer = 0;
	private float duration = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
        if(timer >= duration)
		{
			
			Destroy(this.gameObject);
		}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}

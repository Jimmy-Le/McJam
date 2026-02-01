using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject gameOverPanel;
	[SerializeField] private GameObject rightWall;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

	public void BossKilled()
	{
		rightWall.SetActive(false);
	
	}
	

    public void Leave()
    {
        Application.Quit();
    }
}

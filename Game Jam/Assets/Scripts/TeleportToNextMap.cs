using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToNextMap : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex = 1;  // Drag in Inspector!
    

    
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            LoadNextScene();
        }
    }
    
    public void LoadNextScene() {

        SceneManager.LoadScene(nextSceneIndex);
    }
}

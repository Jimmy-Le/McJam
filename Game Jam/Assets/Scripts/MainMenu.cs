using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class MainMenu : MonoBehaviour
{
    private void Start() 
    {
        MusicManager.Instance.PlayMusic("Menu");


    }




    public void Play()
    {
        SceneManager.LoadScene("Corridor");
        MusicManager.Instance.PlayMusic("Ambiance");
    }


}
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image Heart1;
    public Image Heart2;
    public Image Heart3;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public static PlayerHealth HP;

    void Awake()
    {
        HP = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int playerHealth)
    {
        if (playerHealth == 3)
        {
            Heart1.sprite = fullHeart;
            Heart2.sprite = fullHeart;
            Heart3.sprite = fullHeart;
        }
        
        else if (playerHealth == 2)
        {
            Heart1.sprite = fullHeart;
            Heart2.sprite = fullHeart;
            Heart3.sprite = emptyHeart;
        }
        
        else if (playerHealth == 1) {
            Heart1.sprite = fullHeart;
            Heart2.sprite = emptyHeart;
            Heart3.sprite = emptyHeart;
        }
        
        else if (playerHealth == 0) {
            Heart1.sprite = emptyHeart;
            Heart2.sprite = emptyHeart;
            Heart3.sprite = emptyHeart;
        }
    }
}

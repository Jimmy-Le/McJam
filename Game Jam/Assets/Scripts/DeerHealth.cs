using System;
using UnityEngine;

public class DeerHealth : MonoBehaviour
{
    public Transform bar;
    
    public static DeerHealth DeerHP;
    
    private float maxHealth = 150f;
    void Awake()
    {
        DeerHP = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDeerHealth(maxHealth);
    }

    // Update is called once per frame
    public void UpdateDeerHealth(float health)
    {
        float barWidth = health / maxHealth;
        Vector3 scale = new Vector3(barWidth, 1, 1);
        bar.transform.localScale = scale;
        float barLength = health * 490 / maxHealth;
        bar.transform.localPosition = new Vector3((-245+barLength/2), 180, 0);
    }
}
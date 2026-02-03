using System;
using UnityEngine;

public class DragonHealth : MonoBehaviour
{
    public Transform bar;
    
    public static DragonHealth DragonHP;
	private float maxHealth = 250f;

    void Awake()
    {
        DragonHP = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDragonHealth(maxHealth);
    }

    // Update is called once per frame
    public void UpdateDragonHealth(float health)
    {
        float barWidth = health / maxHealth;
        Vector3 scale = new Vector3(barWidth, 1, 1);
        bar.transform.localScale = scale;
        float barLength = health * 490 / maxHealth;
        bar.transform.localPosition = new Vector3((-245+barLength/2), 180, 0);
    }
}

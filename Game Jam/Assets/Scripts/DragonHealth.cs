using System;
using UnityEngine;

public class DragonHealth : MonoBehaviour
{
    public Transform bar;
    
    public static DragonHealth DragonHP;

    void Awake()
    {
        DragonHP = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDragonHealth(150f);
    }

    // Update is called once per frame
    public void UpdateDragonHealth(float health)
    {
        float barWidth = health / 150f;
        Vector3 scale = new Vector3(barWidth, 1, 1);
        bar.transform.localScale = scale;
        float barLength = health * 490 / 150f;
        bar.transform.localPosition = new Vector3((-245+barLength/2), 185, 0);
        Debug.Log(health);
        Debug.Log(bar.transform.localPosition);
    }
}

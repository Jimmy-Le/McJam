using System;
using UnityEngine;

public class DeerHealth : MonoBehaviour
{
    public Transform bar;
    
    public static DeerHealth DeerHP;

    void Awake()
    {
        DeerHP = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDeerHealth(100f);
    }

    // Update is called once per frame
    public void UpdateDeerHealth(float health)
    {
        float barWidth = health / 100f;
        Vector3 scale = new Vector3(barWidth, 1, 1);
        bar.transform.localScale = scale;
        float barLength = health * 490 / 100f;
        bar.transform.localPosition = new Vector3((-245+barLength/2), 185, 0);
        Debug.Log(health);
        Debug.Log(bar.transform.localPosition);
    }
}

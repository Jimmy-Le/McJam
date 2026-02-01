using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour
{

    public static AbilitiesUI abilitiesUIInstance;
    public Image dashButton;
    public Image flurryButton;
    private Color greyedOut = new Color32(136, 129, 129, 220);
    private Color active = new Color32(255, 255, 255, 255);
    
    
    private void Awake()
    {
        abilitiesUIInstance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateDashUI(float timer)
    {
        if (timer <= 0)
        {
            //ability active
            dashButton.GetComponent<Image>().color= active;
            Debug.Log("updating the UI DASH ACTIVE");
            Debug.Log(dashButton.color);
        }
        else
        {
            //ability inactive
            dashButton.GetComponent<Image>().color = greyedOut;
            Debug.Log("updating the UI greying out the dash");
            Debug.Log(dashButton.color);
            
        }
        
    }

    public void UpdateFlurryUI(float timer, bool hasUnlockedFlurry)
    {
        if (timer <= 0 && hasUnlockedFlurry == true)
        {
            //ability active
            flurryButton.color = active;
            
        }
        else
        {
            //ability inactive
            flurryButton.color = greyedOut;
            
        }
    }
}

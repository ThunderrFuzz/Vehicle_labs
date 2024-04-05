using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public AICar aicar;
    void Update()
    {
        //transform.position = aicar.transform.position; 
    }
    public void UpdateHealthBar(float per)
    {
        healthBar.fillAmount = per;
    }
}

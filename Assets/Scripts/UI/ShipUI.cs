using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShipUI : MonoBehaviour {

    [SerializeField]
    private Image ship;
    
    [SerializeField]
    private float lerpSpeed = 2f;
    private float fillAmount = 1f;
    public float MaxValue { get; set; }
    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }
    void Start () {
		
	}
	
	
	void Update () {
        HandleShipIcon();
    }

    void HandleShipIcon()
    {
        if (fillAmount != ship.fillAmount)
        {
            ship.fillAmount = Mathf.Lerp(ship.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
        
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}

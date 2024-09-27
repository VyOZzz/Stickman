using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heathbar : MonoBehaviour
{
    [SerializeField]private Slider _slider;
    
    public void SetHeath(int heath)
    {
        _slider.value = heath;
    }

    public void SetMaxHeath(int heath)
    {
        _slider.maxValue = heath;
        _slider.value = heath;
    }
    
}

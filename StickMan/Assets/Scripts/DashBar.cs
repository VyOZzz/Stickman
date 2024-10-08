using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    [SerializeField] private Slider sliderDash;
    
    public void SetDashBar(float dashTime)
    {
        sliderDash.value = dashTime;
    }
    public void SetMaxDashBar(float dashTime)
    {
        sliderDash.maxValue = dashTime;
        sliderDash.value = dashTime;
    }
 
    
}

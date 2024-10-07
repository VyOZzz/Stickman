using UnityEngine;

public class PlatformSpecificUI : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject attackButton;
    [SerializeField] private GameObject dashButton;
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            joystick.SetActive(true);
            attackButton.SetActive(true);
            dashButton.SetActive(true);
        }
        else
        {
            joystick.SetActive(false);
            attackButton.SetActive(false);
            dashButton.SetActive(false);
        }
    }
}

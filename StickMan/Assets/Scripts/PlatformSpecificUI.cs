using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class PlatformSpecificUI : MonoBehaviour
{
    
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject attackButton;
    [SerializeField] private GameObject dashButton;
    void Start()
    {
#if UNITY_ANDROID
// Nếu chạy trên Android thì hiển thị joystick và các button cần thiết
            joystick.SetActive(true);
            attackButton.SetActive(true);
            dashButton.SetActive(true);
#endif
#if  UNITY_WINDOWS
            joystick.SetActive(false);
            attackButton.SetActive(false);
            dashButton.SetActive(false);
#endif
    }
}

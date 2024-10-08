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
        // Chạy trên Android
        joystick.SetActive(true);
        attackButton.SetActive(true);
        dashButton.SetActive(true);
#else
        // Tắt trên các nền tảng khác (bao gồm macOS, Windows, Editor)
        joystick.SetActive(false);
        attackButton.SetActive(false);
        dashButton.SetActive(false);
#endif
    }
}

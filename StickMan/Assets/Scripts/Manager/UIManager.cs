using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private GameObject settingPanel;
        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    
        // Start is called before the first frame update
        public void OpenTutorial()
        {
            menuPanel.SetActive(false);
            tutorialPanel.SetActive(true);
            settingPanel.SetActive(false);
        }
        public void QuitTutorial()
        {
            menuPanel.SetActive(true);
            tutorialPanel.SetActive(false);
            settingPanel.SetActive(false);
        }
        public void OpenSetting()
        {
            menuPanel.SetActive(false);
            tutorialPanel.SetActive(false);
            settingPanel.SetActive(true);
        }
        public void QuitSetting()
        {
            menuPanel.SetActive(true);
            tutorialPanel.SetActive(false);
            settingPanel.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }
    
    }
}

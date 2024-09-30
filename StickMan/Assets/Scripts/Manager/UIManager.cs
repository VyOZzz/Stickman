using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private GameObject settingPanel;
        [SerializeField] private GameObject menuLevels;
        public void StartGame()
        {
            menuLevels.SetActive(true);
        }

        public void QuitMenuLevels()
        {
            menuLevels.SetActive(false);
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

        public void Level1()
        {
            SceneManager.LoadScene(1);
        }
        public void Level2()
        {
            SceneManager.LoadScene(2);
        }
        public void Level3()
        {
            SceneManager.LoadScene(3);
        }
    }
}

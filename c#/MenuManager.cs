using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;
    private bool isPaused = false;

    public void LoadScene(int sceneIndex)
    {
        //SceneManager.LoadScene(sceneIndex);
        Debug.Log("0");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Continue();
            else Pause();
            
        }
    }
    public void Pause()
    {
        //AudioManager.PlaySound(SoundType.CLICK);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}

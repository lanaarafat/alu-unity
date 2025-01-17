using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Next()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex < 3)
            SceneManager.LoadScene(sceneIndex + 1);
        else
            MainMenu();
    }
}
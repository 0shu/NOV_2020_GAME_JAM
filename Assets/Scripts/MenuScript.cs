using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Dropdown WindowRes;
    public Dropdown WindowMode;
    bool bFullScreen = true;

    public void PlayGame()
    {
        //SceneManager.LoadScene(); As of now idk what scene to load so after the main game is ready just put the scene i guess
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void ChangeWindow()
    {
        switch (WindowMode.value)
        {
            case 1:
                bFullScreen = false;
                Debug.Log("Windowed");
                break;
            case 2:
                bFullScreen = true;
                Debug.Log("Fullscreen");
                break;
        }
    }

    public void ChangeRes()
    {
        switch (WindowRes.value)
        {
            case 1:
                Screen.SetResolution(800, 600, bFullScreen);
                Debug.Log(Screen.currentResolution);
                break;
            case 2:
                Screen.SetResolution(1280, 720, bFullScreen);
                Debug.Log(Screen.currentResolution);
                break;
            case 3:
                Screen.SetResolution(1360, 768, bFullScreen);
                Debug.Log(Screen.currentResolution);
                break;
            case 4:
                Screen.SetResolution(1366, 768, bFullScreen);
                Debug.Log(Screen.currentResolution);
                break;
            case 5:
                Screen.SetResolution(1920, 1080, bFullScreen);
                Debug.Log(Screen.currentResolution);
                break;
        }
    }
}

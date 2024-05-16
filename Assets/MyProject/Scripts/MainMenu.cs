using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public void OnClick_begin()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClick_quit()
    {
        Application.Quit(0);
    }
}

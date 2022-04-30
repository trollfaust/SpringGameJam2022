using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayButton()
    {
        LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        LoadScene(0);
    }


    private void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }

}

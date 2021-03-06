using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<DialogManager>();
            }

            return _Instance;
        }
    }
    private static DialogManager _Instance;

    public GameObject EndScreen;
    public GameObject HeartUI;
    public TMP_Text EndScreenTitle;
    public GameObject Background;

    public GameObject Tutorial;

    public void Start()
    {
        HeartUI.SetActive(true);
        EndScreen.SetActive(false);

        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            Tutorial.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Tutorial.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void TutorialDone()
    {
        Time.timeScale = 1;
        Tutorial.SetActive(false);
        PlayerPrefs.SetInt("Tutorial", 1);
    }

    public void TriggerEndScreen(bool hasWon)
    {
        HeartUI.SetActive(false);
        EndScreen.SetActive(true);

        string title = "";

        if (hasWon)
        {
            title = "You Won!";
            Background.SetActive(false);
        } else
        {
            title = "You Lost!";
            //Background.SetActive(true);
        }

        EndScreenTitle.text = title;

        FindObjectOfType<CameraLockOn>().IsEndScreen = true;
    }

}

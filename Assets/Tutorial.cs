using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] Pages;

    int currentPage = 0;

    private void Start()
    {
        currentPage = 0;
        SetPageActive(currentPage);
    }

    void NextPage()
    {
        currentPage++;

        if (currentPage >= Pages.Length)
        {
            DialogManager.Instance.TutorialDone();
            return;
        }

        SetPageActive(currentPage);
    }

    void SetPageActive(int index)
    {
        foreach (GameObject item in Pages)
        {
            item.SetActive(false);
        }

        Pages[index].SetActive(true);
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            NextPage();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject Ranker;
    public void StartScene()
    {
        TempData.stageIndex = 0;
        TempData.WeaponLevel = 0;
        InGameManager.score = 0;
        TempData.stageScore = new float[3];
        SceneManager.LoadScene("InGame");
    }

    public void Rangking()
    {
        Ranker.SetActive(true);
    }

    public void Menu()
    {
        menu.SetActive(true);
    }

    public void MenuClose()
    {
        menu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

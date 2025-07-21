using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public GameObject mainMenu;
    public GameObject InGameMenu;
    public GameObject WinMenu;
    public GameObject LostMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (GameManager.instance.currentMenu == Menus.Main)
        {
            mainMenu.SetActive(true);
            InGameMenu.SetActive(false);
            WinMenu.SetActive(false);
            LostMenu.SetActive(false);
        }
        else if (GameManager.instance.currentMenu == Menus.InGame)
        {
            mainMenu.SetActive(false);
            InGameMenu.SetActive(true);
            WinMenu.SetActive(false);
            LostMenu.SetActive(false);
            
        }
        else if (GameManager.instance.currentMenu == Menus.Lost)
        {
            mainMenu.SetActive(false);
            InGameMenu.SetActive(false);
            WinMenu.SetActive(false);
            LostMenu.SetActive(true);
        }
        else if (GameManager.instance.currentMenu == Menus.Win)
        {
            mainMenu.SetActive(false);
            InGameMenu.SetActive(false);
            WinMenu.SetActive(true);
            LostMenu.SetActive(false);
        }
    }

}
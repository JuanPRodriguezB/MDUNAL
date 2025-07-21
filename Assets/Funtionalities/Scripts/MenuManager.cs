
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //Maneja que ocurre con los menus en cada estado de juego

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
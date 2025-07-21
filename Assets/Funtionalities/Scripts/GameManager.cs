using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Menus currentMenu;

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

    public void ToMain()
    {
        currentMenu = Menus.Main;
    }

    public void ToInGame()
    {
        currentMenu = Menus.InGame;
        SceneManager.LoadScene(1);
    }

    public void ToLost()
    {
        currentMenu = Menus.Lost;
    }

    public void ToWin()
    {
        currentMenu = Menus.Win;
    }
}

public enum Menus
{
    Main,
    InGame,
    Lost,
    Win,
}

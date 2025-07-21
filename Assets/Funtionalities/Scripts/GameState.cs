using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject baseGameObject;
    void Update()
    {
        //Permite que este objeto este activo solo si esta en el estado InGame
        if (GameManager.instance.currentMenu == Menus.InGame)
        {
            baseGameObject.SetActive(true);
        }
        else
        {
            baseGameObject.gameObject.SetActive(false); 
        }
    }
}

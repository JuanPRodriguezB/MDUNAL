using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject baseGameObject;
    void Update()
    {
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

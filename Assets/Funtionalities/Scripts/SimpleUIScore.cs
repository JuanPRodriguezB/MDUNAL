using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleUIScore : MonoBehaviour
{
    public TextMeshProUGUI scoreWin;
    public TextMeshProUGUI scoreLost;

    private void Update()
    {
        if (GameManager.instance.currentMenu == Menus.InGame)
        {
            scoreWin.text = HealthCounter.Instance.score.text;
            scoreLost.text = HealthCounter.Instance.score.text;
        }
    }
}

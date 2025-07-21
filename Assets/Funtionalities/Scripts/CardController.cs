using Microlight.MicroBar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Camera cam;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] int points = 0;
    [SerializeField] MicroBar healthBar;
    [SerializeField] bool correctCard = true;

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("H").GetComponent<MicroBar>();
        score = GameObject.FindGameObjectWithTag("S").GetComponent<TextMeshProUGUI>();
        healthBar.Initialize(3);
    }

    private void OnMouseOver()
    {
        
        if (Input.GetMouseButton(0))
        {
            
            if (correctCard)
            {
                CorrectCardSelected();
                Proposition.Instance.RevealNextSymbol();
                this.gameObject.SetActive(false); 
            }
            else if (!correctCard) 
            {
                WrongCardSelected();
            }


            //MoveCardNotWorking();
        }
        else
        {
            animator.SetBool("MouseOver", true);
            animator.SetBool("win", false);
            animator.SetBool("wrong", false);
            animator.SetBool("idle", false);
            animator.SetBool("click", false);
        }
    }

    private void MoveCardNotWorking()
    {
        animator.SetBool("click", true);
        animator.SetBool("MouseOver", false);
        animator.SetBool("win", false);
        animator.SetBool("wrong", false);
        animator.SetBool("idle", false);
        Vector3 mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        mousePos.z = 0;
        this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
    }

    private void CorrectCardSelected()
    {
        animator.SetBool("win", true);
        animator.SetBool("click", false);
        animator.SetBool("MouseOver", false);
        animator.SetBool("wrong", false);
        animator.SetBool("idle", false);
        points += 500;
        score.text = "score: " + points.ToString();
    }

    private void WrongCardSelected()
    {
        animator.SetBool("wrong", true);
        animator.SetBool("click", false);
        animator.SetBool("MouseOver", false);
        animator.SetBool("win", false);
        animator.SetBool("idle", false);
        points -= 500;
        score.text = "score: " + points.ToString();
        healthBar.UpdateBar(-1);
    }

    private void OnMouseDrag()
    {
        animator.SetBool("click", true);
        animator.SetBool("MouseOver", false);
        animator.SetBool("win", false);
        animator.SetBool("wrong", false);
        animator.SetBool("idle", false);
    }

    private void OnMouseExit()
    {
        animator.SetBool("idle", true);
        animator.SetBool("click", false);
        animator.SetBool("MouseOver", false);
        animator.SetBool("win", false);
        animator.SetBool("wrong", false);
    }

}

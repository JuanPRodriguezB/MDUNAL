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
    [SerializeField] bool interactable = true;
    [SerializeField] string connector;
    [SerializeField] int pointsAdded;

    [SerializeField] GameObject neg;
    [SerializeField] GameObject andor1;
    [SerializeField] GameObject andor2;
    [SerializeField] GameObject imp;
    [SerializeField] GameObject dimp;
    [SerializeField] GameObject baseCard;
    [SerializeField] TextMeshProUGUI AddedPoints;


    private void Start()
    {
        addConnector();
    }

    private void Update()
    {
        
    }

    public void addConnector()
    {
        if (Proposition.Instance.intialHiddenSymbols.Count > 0)
        {
            int randomI = Random.Range(
                0,
                Proposition.Instance.intialHiddenSymbols.Count);
            string randomSymbol = Proposition.Instance.intialHiddenSymbols[randomI];
            if (randomSymbol == "¬")
            {
                neg.SetActive(true);
            }
            else if (randomSymbol == "∧" )
            {
                andor1.SetActive(true);
            }
            else if (randomSymbol == "∨")
            {
                andor2.SetActive(true);
            }
            else if (randomSymbol == "⇒" )
            {
                imp.SetActive(true);
            }
            else if (randomSymbol == "⇔")
            {
                dimp.SetActive(true);
            }
            connector = randomSymbol;
            Proposition.Instance.intialHiddenSymbols.RemoveAt(randomI);
        }
    }

    private void OnMouseOver()
    {
        
        if (Input.GetMouseButton(0) && interactable)
        {
            if (connector == Proposition.Instance.HiddenSymbols.Peek())
            {
                CorrectCardSelected();
                interactable = false;
                Proposition.Instance.RevealNextSymbol();
                Invoke("DestroyGameObject", 0.4f);
            }
            else
            {
                WrongCardSelected();
                interactable = false;
                Invoke("MakeInteractable" ,0.4f);
            }
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

    private void MakeInteractable()
    {
        interactable = true;
    }

    private void DestroyGameObject()
    {
        Destroy(baseCard);
    }

    private void CorrectCardSelected()
    {
        pointsAdded = HealthCounter.Instance.RegisterCorrectAnswer(TimeLeft.instance.TimeElapsedSinceLastWin);
        AddedPoints.text = "+" + pointsAdded.ToString();
        animator.SetBool("win", true);
        animator.SetBool("click", false);
        animator.SetBool("MouseOver", false);
        animator.SetBool("wrong", false);
        animator.SetBool("idle", false);
        TimeLeft.instance.TimeElapsedSinceLastWin = 0;
        HealthCounter.Instance.score.text = "score: " + HealthCounter.Instance.points.ToString();
    }

    private void WrongCardSelected()
    {
        animator.SetBool("wrong", true);
        animator.SetBool("click", false);
        animator.SetBool("MouseOver", false);
        animator.SetBool("win", false);
        animator.SetBool("idle", false);
        HealthCounter.Instance.points -= 500;
        TimeLeft.instance.TimeElapsedSinceLastWin = 0;
        HealthCounter.Instance.score.text = "score: " + HealthCounter.Instance.points.ToString();
        HealthCounter.Instance.currentLife -= 1;
        HealthCounter.Instance.healthBar.UpdateBar(HealthCounter.Instance.currentLife);
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


public enum connectors
{
    Neg,
    AndOr1,
    AndOr2,
    Imp,
    Dimp
}
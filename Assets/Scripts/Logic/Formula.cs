using System.Collections.Generic;
using System.Text;
using UnityEngine;


/// Representa una fórmula lógica como una secuencia de cartas.
public class Formula
{
    // Lista de cartas que componen la fórmula
    public List<Card> cards;

    public Formula()
    {
        cards = new List<Card>();
    }


    /// Agrega una carta a la fórmula.
    public void AddCard(Card card)
    {
        cards.Add(card);
    }


    /// Devuelve la fórmula como string legible (ej: ¬p ∨ q)
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach (Card card in cards)
        {
            sb.Append(card.displayValue).Append(" ");
        }

        return sb.ToString().TrimEnd();
    }


    /// Limpia todas las cartas de la fórmula.
    public void Clear()
    {
        cards.Clear();
    }


    /// Devuelve las variables proposicionales usadas.
    public List<string> GetVariables()
    {
        List<string> vars = new List<string>();

        foreach (Card card in cards)
        {
            if (card.IsVariable() && !vars.Contains(card.displayValue))
            {
                vars.Add(card.displayValue);
            }
        }

        return vars;
    }


    /// Devuelve true si la fórmula está vacía.
    public bool IsEmpty()
    {
        return cards.Count == 0;
    }


    /// Devuelve la cantidad de operadores en la fórmula.
    public int CountOperators()
    {
        int count = 0;

        foreach (Card card in cards)
        {
            if (card.IsOperator()) count++;
        }

        return count;
    }
}


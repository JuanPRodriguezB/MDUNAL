using System.Collections.Generic;
using UnityEngine;


/// Clase que valida si una fórmula lógica es bien formada (WFF).
public static class FormulaValidator
{

    /// Devuelve true si la fórmula es bien formada.
    public static bool IsWellFormed(Formula formula)
    {
        if (formula.IsEmpty()) return false;

        List<Card> cards = formula.cards;
        int operandCount = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            Card current = cards[i];

            // Caso 1: Si es variable, debe actuar como operando
            if (current.IsVariable())
            {
                operandCount++;
                continue;
            }

            // Caso 2: Si es operador
            if (current.IsOperator())
            {
                switch (current.operatorType)
                {
                    case OperatorType.Negation:
                        // ¬ debe ir seguido de variable u otra ¬
                        if (i == cards.Count - 1) return false; // nada después
                        if (!cards[i + 1].IsVariable() &&
                            !(cards[i + 1].IsOperator() && cards[i + 1].operatorType == OperatorType.Negation))
                            return false;
                        break;

                    default:
                        // Operadores binarios: ∧, ∨, →, ↔
                        // Deben estar rodeados por operandos válidos
                        if (i == 0 || i == cards.Count - 1) return false;

                        Card prev = cards[i - 1];
                        Card next = cards[i + 1];

                        if (!prev.IsVariable() && !(prev.IsOperator() && prev.operatorType == OperatorType.Negation))
                            return false;

                        if (!next.IsVariable() && !(next.IsOperator() && next.operatorType == OperatorType.Negation))
                            return false;
                        break;
                }
            }
        }

        // Una fórmula mínima debe tener al menos un operando
        return operandCount > 0;
    }
}


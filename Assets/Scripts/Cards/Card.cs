using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardType
    {
        Variable,
        Operator
    }

    [Header("Card Info")]
    public CardType cardType;
    public string displayValue; // Lo que se muestra en la carta

    [Header("Operator Info (if applicable)")]
    public OperatorType operatorType;  // Ahora usa el enum global
    public int precedence; // Precedencia lógica (0 más bajo, mayor = más prioritario)


    /// Configura la carta como variable proposicional.
    public void SetupVariable(string variableName)
    {
        cardType = CardType.Variable;
        displayValue = variableName;
        precedence = -1; // No aplica
    }


    /// Configura la carta como operador lógico.
    public void SetupOperator(OperatorType type)
    {
        cardType = CardType.Operator;
        operatorType = type;
        displayValue = GetSymbolForOperator(type);
        precedence = GetPrecedence(type);
    }

    private string GetSymbolForOperator(OperatorType type)
    {
        return type switch
        {
            OperatorType.Negation => "¬",
            OperatorType.Conjunction => "∧",
            OperatorType.Disjunction => "∨",
            OperatorType.Implication => "→",
            OperatorType.Biconditional => "↔",
            _ => "?"
        };
    }

    private int GetPrecedence(OperatorType type)
    {
        // Asignamos precedencias comunes
        return type switch
        {
            OperatorType.Negation => 4,
            OperatorType.Conjunction => 3,
            OperatorType.Disjunction => 2,
            OperatorType.Implication => 1,
            OperatorType.Biconditional => 0,
            _ => -1
        };
    }


    /// Devuelve si la carta es una variable.
    public bool IsVariable()
    {
        return cardType == CardType.Variable;
    }


    /// Devuelve si la carta es un operador.
    public bool IsOperator()
    {
        return cardType == CardType.Operator;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject cardPrefab;
    public Transform spawnArea;

    [Header("Configuración de dificultad")]
    public int currentLevel = 1;
    public int minLength = 3;
    public int maxLength = 5;

    private Formula targetFormula;

    void Start()
    {
        StartNewRound();
    }

    public void StartNewRound()
    {
        Formula formula = GenerateRandomFormula();
        targetFormula = formula;

        // Mostrar fórmula desordenada al jugador
        DisplayShuffledFormula(formula);
    }

    private Formula GenerateRandomFormula()
    {
        Formula formula = new Formula();
        int length = Random.Range(minLength, maxLength + 1);

        List<OperatorType> availableOperators = GetAllowedOperators(currentLevel);
        List<string> variables = new List<string> { "p", "q", "r", "s" };

        int i = 0;
        while (i < length)
        {
            int choice = Random.Range(0, 2); // 0 = variable, 1 = operador

            if (choice == 0 || i == length - 1) // Último siempre variable
            {
                string varName = variables[Random.Range(0, variables.Count)];
                Card variableCard = CreateCard(Card.CardType.Variable, varName);
                formula.AddCard(variableCard);
                i++;
            }
            else
            {
                OperatorType op = availableOperators[Random.Range(0, availableOperators.Count)];
                Card operatorCard = CreateCard(Card.CardType.Operator, op);
                formula.AddCard(operatorCard);
                i++;
            }
        }

        return formula;
    }

    private List<OperatorType> GetAllowedOperators(int level)
    {
        List<OperatorType> operators = new List<OperatorType> { OperatorType.Negation };

        if (level >= 2) operators.Add(OperatorType.Conjunction);
        if (level >= 3) operators.Add(OperatorType.Disjunction);
        if (level >= 4) operators.Add(OperatorType.Implication);
        if (level >= 5) operators.Add(OperatorType.Biconditional);

        return operators;
    }

    private void DisplayShuffledFormula(Formula formula)
    {
        List<Card> cards = new List<Card>(formula.cards);
        cards.Shuffle(); // Necesitas una extensión para esto

        float x = -cards.Count / 2f;

        foreach (Card card in cards)
        {
            GameObject obj = Instantiate(cardPrefab, spawnArea);
            obj.transform.localPosition = new Vector3(x * 100f, 0, 0);
            obj.GetComponent<Card>().SetupOperator(card.operatorType); // o SetupVariable
            x += 1f;
        }
    }

    private Card CreateCard(Card.CardType type, object value)
    {
        GameObject obj = Instantiate(cardPrefab);
        Card card = obj.GetComponent<Card>();

        if (type == Card.CardType.Variable)
            card.SetupVariable((string)value);
        else
            card.SetupOperator((OperatorType)value);

        return card;
    }
}


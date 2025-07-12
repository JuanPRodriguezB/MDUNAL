// using System.Collections.Generic;
// using UnityEngine;

// public class CardFactory : MonoBehaviour
// {
//     [Header("Prefab de carta")]
//     public GameObject cardPrefab;

//     [Header("Opciones de variables")]
//     public List<string> variableNames = new List<string> { "p", "q", "r", "s" };


//     /// Crea una carta de variable.
//     public Card CreateVariableCard(string variableName)
//     {
//         GameObject obj = Instantiate(cardPrefab);
//         Card card = obj.GetComponent<Card>();
//         card.SetupVariable(variableName);
//         return card;
//     }


//     /// Crea una carta de operador lógico.
//     public Card CreateOperatorCard(OperatorType type)
//     {
//         GameObject obj = Instantiate(cardPrefab);
//         Card card = obj.GetComponent<Card>();
//         card.SetupOperator(type);
//         return card;
//     }


//     /// Crea una carta aleatoria (operador o variable).
//     public Card CreateRandomCard()
//     {
//         bool createOperator = Random.value < 0.5f;

//         if (createOperator)
//         {
//             OperatorType randomType = (OperatorType)Random.Range(0, System.Enum.GetNames(typeof(OperatorType)).Length);
//             return CreateOperatorCard(randomType);
//         }
//         else
//         {
//             string randomVar = variableNames[Random.Range(0, variableNames.Count)];
//             return CreateVariableCard(randomVar);
//         }
//     }


//     /// Crea una secuencia aleatoria de cartas.
//     public List<Card> CreateRandomFormula(int length)
//     {
//         List<Card> formula = new List<Card>();
//         for (int i = 0; i < length; i++)
//         {
//             formula.Add(CreateRandomCard());
//         }
//         return formula;
//     }
// }


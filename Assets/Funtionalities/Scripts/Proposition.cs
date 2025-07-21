using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Proposition : MonoBehaviour
{
    public static Proposition Instance;

    [Header("Expresión lógica original")]
    [TextArea(2, 5)]
    public string originalExpression = "¬P ∧ Q ⇒ R";

    [Header("Componente TextMeshPro para mostrar el resultado")]
    public TextMeshProUGUI targetText;

    private string currentExpression;
    [SerializeField] private Queue<string> hiddenSymbols = new Queue<string>();
    private const string placeholder = "(___)";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnEnable()
    {
        ProcessExpression();
        UpdateText();
    }

    /// <summary>
    /// Procesa el string original: guarda los símbolos y los reemplaza por (___)
    /// </summary>
    public void ProcessExpression()
    {
        hiddenSymbols.Clear();

        string[] logicSymbols = { "¬", "∧", "∨", "⇒", "⇔" };
        currentExpression = originalExpression;

        foreach (string symbol in logicSymbols)
        {
            int index;
            while ((index = currentExpression.IndexOf(symbol)) != -1)
            {
                currentExpression = currentExpression.Substring(0, index) + placeholder + currentExpression.Substring(index + symbol.Length);
                hiddenSymbols.Enqueue(symbol);
            }
        }
    }

    /// <summary>
    /// Reemplaza el primer (___) por el símbolo correspondiente
    /// </summary>
    public void RevealNextSymbol()
    {
        if (hiddenSymbols.Count > 0 && currentExpression.Contains(placeholder))
        {
            string symbolToInsert = hiddenSymbols.Dequeue();
            int index = currentExpression.IndexOf(placeholder);
            currentExpression = currentExpression.Remove(index, placeholder.Length).Insert(index, symbolToInsert);
            UpdateText();
        }
        else
        {
            Debug.Log("No hay más símbolos por revelar.");
        }
    }

    /// <summary>
    /// Actualiza el texto mostrado en el TextMeshPro
    /// </summary>
    private void UpdateText()
    {
        if (targetText != null)
        {
            targetText.text = currentExpression;
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI no asignado.");
        }
    }
}

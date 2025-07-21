using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Proposition : MonoBehaviour
{
    public static Proposition Instance;

    [SerializeField] int count = 10;
    [SerializeField] int buCount = 10;

    [Header("Expresión lógica original")]
    [TextArea(2, 5)]
    public string originalExpression = "¬P ∧ Q ⇒ R";

    public List<string> strings = new List<string>();

    [Header("Componente TextMeshPro para mostrar el resultado")]
    public TextMeshProUGUI targetText;

    private string currentExpression;
    [SerializeField] private Queue<string> hiddenSymbols = new Queue<string>();
    [SerializeField] public List<string> intialHiddenSymbols = new List<string>();
    private const string placeholder = "(___)";

    public Queue<string> HiddenSymbols { get => hiddenSymbols; set => hiddenSymbols = value; }

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
        targetText = GameObject.FindGameObjectWithTag("T").GetComponent<TextMeshProUGUI>();

        originalExpression = strings[Random.Range(0, strings.Count)];
        ProcessExpression();
        UpdateText();
    }

    void Start()
    {
    }

    private void Update()
    {
        if (HiddenSymbols.Count <= 0)
        {
            count--;
            originalExpression = strings[Random.Range(0, strings.Count)];
            ProcessExpression();
            UpdateText();
            VisualCardPos.Instance.numberOfCards = Proposition.Instance.intialHiddenSymbols.Count;
            VisualCardPos.Instance.SpawnHand();
            HealthCounter.Instance.currentLife = (int)HealthCounter.Instance.healthBar.MaxValue;
            HealthCounter.Instance.healthBar.UpdateBar(HealthCounter.Instance.currentLife);
        }
        if (count <= 0)
        {
            count = buCount;
            GameManager.instance.ToWin();
        }
    }

    public void checkCorrectCard(string cardSimbol)
    {
        if (cardSimbol == HiddenSymbols.Peek())
        {
            RevealNextSymbol();
        }
    }

    /// <summary>
    /// Procesa el string original: guarda los símbolos y los reemplaza por (___),
    /// en el orden en que aparecen en el string original.
    /// </summary>
    public void ProcessExpression()
    {
        HiddenSymbols.Clear();
        currentExpression = "";

        string[] logicSymbols = { "¬", "∧", "∨", "⇒", "⇔" };
        int i = 0;

        while (i < originalExpression.Length)
        {
            bool matched = false;

            // Verifica si alguno de los símbolos lógicos inicia en la posición actual
            foreach (string symbol in logicSymbols)
            {
                if (originalExpression.Substring(i).StartsWith(symbol))
                {
                    HiddenSymbols.Enqueue(symbol);
                    intialHiddenSymbols.Add(symbol);
                    currentExpression += placeholder;
                    i += symbol.Length;
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                currentExpression += originalExpression[i];
                i++;
            }
        }
    }

    /// <summary>
    /// Reemplaza el primer (___) por el símbolo correspondiente
    /// </summary>
    public void RevealNextSymbol()
    {
        if (HiddenSymbols.Count > 0 && currentExpression.Contains(placeholder))
        {
            string symbolToInsert = HiddenSymbols.Dequeue();
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
    public void UpdateText()
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

using UnityEngine;

class LatexFunctions : MonoBehaviour
{
    private Dictionary<string, string> latexSyntax;

    void Start()
    {
        InitializeLatexSyntax();
    }

    private void InitializeLatexSyntax()
    {
        latexSyntax = new Dictionary<string, string>
        {
            { "¬", "\\neg" },
            { "∧", "\\land" },
            { "∨", "\\lor" },
            { "→", "\\implies" },
            { "↔", "\\iff" }
        };
    }


    private Formula CreateFormulaFromLatex(string latexString)
    {
        Debug.Log("Converting LaTeX expression: " + latexString);

        Formula formula = new Formula();
        string normalString = latexString.ToLower();

        foreach (var latexOperator in latexSyntax)
        {
            normalString = normalString.Replace(latexOperator.Value, latexOperator.Key);
            Debug.Log($"Replaced {latexOperator.Value} with {latexOperator.Key}");
        }

        Debug.Log("Converted string: " + normalString);

        string[] stringParts = latexString.Split(' ');

        foreach (string part in stringParts)
        {
            switch (part)
            {
                case "\\neg":
                    formula.AddCard(CreateCard(Card.CardType.Operator, OperatorType.Negation));
                    break;
                case "\\land":
                    formula.AddCard(CreateCard(Card.CardType.Operator, OperatorType.Conjunction));
                    break;
                case "\\lor":
                    formula.AddCard(CreateCard(Card.CardType.Operator, OperatorType.Disjunction));
                    break;
                case "\\implies":
                    formula.AddCard(CreateCard(Card.CardType.Operator, OperatorType.Implication));
                    break;
                case "\\iff":
                    formula.AddCard(CreateCard(Card.CardType.Operator, OperatorType.Biconditional));
                    break;
                default:
                    formula.AddCard(CreateCard(Card.CardType.Variable, part));
                    break;
            }
        }
        Debug.Log("Created formula from LaTeX: " + formula.ToString());
        return formula;
    }

    private Formula CreateLatexFromFormula(Formula formula)
    {
        Debug.Log("Creating formula from LaTeX: " + formula.ToString());

        StringBuilder latexString = new StringBuilder();
        string[] formulaParts = formula.ToString().ToLower().Split(' ');

        foreach (string part in formulaParts)
        {
            switch (part)
            {
                case "¬":
                    latexString.Append("\\neg ");
                    break;
                case "∧":
                    latexString.Append("\\land ");
                    break;
                case "∨":
                    latexString.Append("\\lor ");
                    break;
                case "→":
                    latexString.Append("\\implies ");
                    break;
                case "↔":
                    latexString.Append("\\iff ");
                    break;
                default:
                    latexString.Append(part+" ");
                    break;
            }
        }
        return latexString.ToString();
    }
}
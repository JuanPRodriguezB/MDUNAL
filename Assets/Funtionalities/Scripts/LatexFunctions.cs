using System.Collections.Generic;
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
            { "¬", " \\neg " },
            { "∧", " \\land " },
            { "∨", " \\lor " },
            { "→", " \\implies " },
            { "⇒", " \\implies " },
            { "↔", " \\iff " },
            { "⇔", " \\iff " }
        };
    }

    private string CreateLatexFromFormula(string formula)
    {
        string latexString = "";

        foreach (var latexOperator in latexSyntax)
        {
            formula = formula.Replace(latexOperator.Key, latexOperator.Value);
        }


        string[] formulaParts = formula.ToLower().Split(' ');

        foreach (string part in formulaParts)
        {
            switch (part)
            {
                case "¬":
                    latexString = latexString + " \\neg ";
                    break;
                case "∧":
                    latexString = latexString + " \\land ";
                    break;
                case "∨":
                    latexString = latexString + " \\lor ";
                    break;
                case "→":
                    latexString = latexString + " \\implies ";
                    break;
                case "⇒":
                    latexString = latexString + " \\implies ";
                    break;
                case "↔":
                    latexString = latexString + " \\iff ";
                    break;
                case "↔":
                    latexString = latexString + " \\iff ";
                    break;
                default:
                    latexString = latexString+" "+part+" ";
                    break;
            }
        }
        return latexString;
    }
}
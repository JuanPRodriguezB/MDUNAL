using System.Collections.Generic;

public static class FormulaParser
{
    /// <summary>
    /// Parsea la fórmula en una estructura de árbol de sintaxis (AST).
    /// </summary>
    public static FormulaNode ParseFormula(Formula formula)
    {
        if (formula == null || formula.IsEmpty())
            return null;

        List<Card> tokens = formula.cards;

        // Paso 1: Convertir a notación postfija (usando shunting-yard)
        List<Card> postfix = ConvertToPostfix(tokens);

        // Paso 2: Construir el árbol desde la notación postfija
        return BuildSyntaxTree(postfix);
    }

    private static List<Card> ConvertToPostfix(List<Card> input)
    {
        List<Card> output = new List<Card>();
        Stack<Card> stack = new Stack<Card>();

        foreach (Card token in input)
        {
            if (token.IsVariable())
            {
                output.Add(token);
            }
            else if (token.IsOperator())
            {
                while (stack.Count > 0 && stack.Peek().IsOperator() &&
                    stack.Peek().precedence >= token.precedence)
                {
                    output.Add(stack.Pop());
                }

                stack.Push(token);
            }
        }

        while (stack.Count > 0)
        {
            output.Add(stack.Pop());
        }

        return output;
    }

    private static FormulaNode BuildSyntaxTree(List<Card> postfix)
    {
        Stack<FormulaNode> stack = new Stack<FormulaNode>();

        foreach (Card token in postfix)
        {
            if (token.IsVariable())
            {
                stack.Push(new FormulaNode(token));
            }
            else if (token.IsOperator())
            {
                FormulaNode node = new FormulaNode(token);

                if (token.operatorType == OperatorType.Negation)
                {
                    node.right = stack.Pop(); // un solo hijo derecho
                }
                else
                {
                    node.right = stack.Pop();
                    node.left = stack.Pop();
                }

                stack.Push(node);
            }
        }

        return stack.Count > 0 ? stack.Pop() : null;
    }
}


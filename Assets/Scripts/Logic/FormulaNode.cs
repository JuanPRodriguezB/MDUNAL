public class FormulaNode
{
    public Card card; // La carta asociada a este nodo
    public FormulaNode left;
    public FormulaNode right;

    public FormulaNode(Card card)
    {
        this.card = card;
    }

    public bool IsLeaf()
    {
        return left == null && right == null;
    }
}


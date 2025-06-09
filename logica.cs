using System;
using System.Collections.Generic;
class Logica {

  static void Main()
  {

    Dictionary<string, string> latexSyntax = new Dictionary<string, string>();

    latexSyntax.Add("¬", "\\neg");
    latexSyntax.Add("Y", "\\land");
    latexSyntax.Add("O", "\\lor");
    latexSyntax.Add("->", "\\implies");
    latexSyntax.Add("<->", "\\iff");

    Console.WriteLine("Hello World");

    Console.WriteLine("Enter LaTeX expression:");
    string latexString = Console.ReadLine();
    latexString = latexString.ToLower();
    Console.WriteLine("The LaTeX string is:"+ latexString);

    string normalString = latexString;

    // Replace LaTeX operators with their corresponding symbols
    foreach (var latexOperator in latexSyntax)
    {
      normalString = normalString.Replace(latexOperator.Value, latexOperator.Key);
      Console.WriteLine(latexOperator);
    }

    Console.WriteLine("The base string is: " + normalString);
    
  }
}
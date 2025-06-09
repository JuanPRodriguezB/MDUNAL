using System;
using System.Collections.Generic;
class Logica {
    
  static void Main() {
      
     Dictionary<string, string> latexSyntax = new Dictionary<string, string>();
      
    latexSyntax.Add ("¬", "\\neg");
    latexSyntax.Add ("Y", "\\land");
    latexSyntax.Add ("O", "\\lor");
    latexSyntax.Add ("->","\\implies");
    latexSyntax.Add ("<->","\\iff");
    
    Console.WriteLine("Hello World");
    
    Console.WriteLine("Enter LaTeX expression:");
    string latexString = Console.ReadLine();
    latexString = latexString.ToLower();
    
    foreach (var latexOperator in latexSyntax) 
    {  
        latexString = latexString.Replace(latexOperator.Value ,latexOperator.Key);
        Console.WriteLine(latexOperator);
    }
    
    
    Console.WriteLine(latexString);
  }
}
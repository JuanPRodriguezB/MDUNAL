using System;
using System.Collections.Generic;
class HelloWorld
{



    static void Main()
    {

        Dictionary<string, string> latexSyntax = new Dictionary<string, string>();

        latexSyntax.Add("¬", "\\neg");
        latexSyntax.Add("Y", "\\land");
        latexSyntax.Add("O", "\\lor");
        latexSyntax.Add("->", "\\implies");
        latexSyntax.Add("<->", "\\iff");

        Console.WriteLine("Hello World");



        foreach (var pair in latexSyntax)
        {
            Console.WriteLine(pair);
        }

    }
}
  
  
    

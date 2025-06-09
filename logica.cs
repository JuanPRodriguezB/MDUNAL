using System;
using System.Collections.Generic;
class Logica
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

    Console.WriteLine("Enter LaTeX expression:");
    string latexString = Console.ReadLine();
    latexString = latexString.ToLower();
    Console.WriteLine("The LaTeX string is:" + latexString);

    string normalString = latexString;

    // Replace LaTeX operators with their corresponding symbols
    foreach (var latexOperator in latexSyntax)
    {
      normalString = normalString.Replace(latexOperator.Value, latexOperator.Key);
      Console.WriteLine(latexOperator);
    }

    Console.WriteLine("The base string is: " + normalString);

    Console.WriteLine("Next, the operators priority will be selected.");

    SortedList<int, string> orderOperator = new SortedList<int, string>();

    foreach (var latexOperator in latexSyntax)
    {
      Console.WriteLine("Enter the priority for operator " + latexOperator.Key);

      orderOperator.Add(int.Parse(Console.ReadLine()), latexOperator.Key);


    }

    Console.WriteLine("Here is the final order: " + normalString);

    foreach (var temp in orderOperator)
    {
      Console.WriteLine(temp.Key + ": " + temp.Value);
    }

    //Console.WriteLine("By default, the program will reformulate the equation from left to right."
    //+"\n Should the program do so from right to left? (y/n): ");
    //switch(Console.ReadLine())

    //Console.WriteLine("Working on it...");

    /*
    for (int i = 1; i <= 4; i = i++) 
    {
        
        int stringLength = normalString.Length;
        for (int j=0; j<=stringLength; j=j++);
        {
            
        }
        
        Console.WriteLine(temp.Key + ": " + temp.Value);
        Console.WriteLine(i);
        
        
    }
    */

  }
}
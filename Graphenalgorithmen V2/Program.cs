using Graphenalgorithmen_V2;

const string TITLE = "Graphenalgorithmen";
Console.Title = TITLE;
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine($"{TITLE}, {System.Environment.MachineName}, Moritz Troestl");

Manager manager = new Manager();
manager.UseGraph();

Console.WriteLine("press any key to continue . . .");
Console.ReadKey(true);
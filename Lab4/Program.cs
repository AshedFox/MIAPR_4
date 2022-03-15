using Lab4.Entity;

Console.Write("Enter amount of classes: ");
var classesCount = Console.ReadLine();

Console.Write("Enter amount of objects: ");
var objectsCount = Console.ReadLine();

Console.Write("Enter amount of attributes per object: ");
var attributesCount = Console.ReadLine();

var perceptron = new Perceptron(Convert.ToInt32(classesCount), Convert.ToInt32(objectsCount),
    Convert.ToInt32(attributesCount));
    
Console.WriteLine("--------------------GENERATED CLASSES--------------------");
perceptron.PrintClassesStrings();

perceptron.Process();

Console.WriteLine("--------------------FINAL FUNCTIONS--------------------");
perceptron.PrintFunctionsStrings();
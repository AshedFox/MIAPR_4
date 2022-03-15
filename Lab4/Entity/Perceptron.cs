using System.Collections.ObjectModel;
using System.Text;

namespace Lab4.Entity;

public class Perceptron
{
    private readonly int _objectsCount;
    public List<Class> Classes { get; } = new ();
    public List<Weight> Weights { get; } = new ();
    private readonly int _min = -10;
    private readonly int _max = 10;
    private readonly int _maxIterations = 10000;

    public Perceptron(int classesCount, int objectsCount, int attributesCount)
    {
        _objectsCount = objectsCount;
        //Classes = GetTestClasses; 
        GenerateClasses(classesCount, objectsCount, attributesCount);
        InitWeights(classesCount, attributesCount + 1);
    }

    private List<Class> GetTestClasses => new List<Class>()
    {
        new ()
        {
            Objects = new List<Object>()
            {
                new ()
                {
                    Attributes = { 0, 0, 1 }
                }
            }
        },
        new ()
        {
            Objects = new List<Object>()
            {
                new ()
                {
                    Attributes = {1,1,1}
                }
            }
        },
        new ()
        {
            Objects = new List<Object>()
            {
                new ()
                {
                    Attributes = {-1,1, 1}
                }
            }
        }
    };

    /*private bool IsCorrect(bool[,] isCorrect)
    {
        foreach (var b in isCorrect)
        {
            if (!b)
            {
                return false;
            }
        }

        return true;
    }

    private void ResetIsCorrect(ref bool[,] isCorrect)
    {
        for (int i = 0; i < isCorrect.GetLength(0); i++)
        {
            for (int j = 0; j < isCorrect.GetLength(1); j++)
            {
                isCorrect[i, j] = false;
            }
        }
    }*/
    
    public void Process(bool withTempOutput = false)
    {
        var isCorrect = false;

        var i = 0;
        var functions = new int[Weights.Count];
        var currentIteration = 0;

        var limit = _maxIterations;

        while (!isCorrect && limit > 0)
        {
            isCorrect = i == 0;

            for (var j = 0; j < _objectsCount; j++)
            {
                if (withTempOutput)
                {
                    Console.WriteLine(
                        $"--------------------------FUNCTIONS #{currentIteration}--------------------------");
                    PrintFunctionsStrings();
                }

                var shouldAdd = false;

                for (var k = 0; k < Weights.Count; k++)
                {
                    functions[k] = MultiplyVectors(Weights[k].Values, Classes[i].Objects[j].Attributes);
                }
                
                for (var k = 0; k < functions.Length; k++)
                {
                    if (i == k)
                    {
                        continue;
                    }

                    if (functions[i] <= functions[k])
                    {
                        Weights[k].Values = SubVector(Weights[k].Values, Classes[i].Objects[j].Attributes);
                        isCorrect = false;
                        shouldAdd = true;
                    }
                }

                if (shouldAdd)
                {
                    Weights[i].Values = AddVector(Weights[i].Values, Classes[i].Objects[j].Attributes);
                }

                currentIteration++;
            }

            i = (i + 1) % Classes.Count;
            limit--;
        }

        if (limit == 0)
        {
            Console.WriteLine("Result might be inaccurate");
        }
    }
    
    public List<string> GenerateClassesStrings()
    {
        var classesStrings = new List<string>();

        foreach (var @class in Classes)
        {
            var builder = new StringBuilder("CLASS { ");
            foreach (var @object in @class.Objects)
            {
                builder.Append("OBJECT { ");

                foreach (var attribute in @object.Attributes)
                {
                    builder.Append($"{attribute};");
                }
                builder.Append(" }; ");
            }

            builder.Append(" }");
            classesStrings.Add(builder.ToString());
        }

        return classesStrings;
    }

    public void PrintClassesStrings()
    {
        var classesStrings = GenerateClassesStrings();
        foreach (var classString in classesStrings)
        {
            Console.WriteLine(classString);
        }
    }

    public List<string> GenerateFunctionsStrings()
    {
        var functionsStrings = new List<string>();
        
        for (var i = 0; i < Classes.Count; i++)
        {
            var builder = new StringBuilder($"d{i + 1}(x) = ");

            for (var j = 0; j < Weights[i].Values.Count - 1; j++)
            {
                if (j == 0 && Weights[i].Values[j] < 0)
                {
                    builder.Append("-");
                }
                
                if (Weights[i].Values[j + 1] >= 0)
                {
                    builder.Append($"{Math.Abs(Weights[i].Values[j])}*x{j + 1} + ");
                }
                else
                {
                    builder.Append($"{Math.Abs(Weights[i].Values[j])}*x{j + 1} - ");
                }
            }

            builder.Append($"{Math.Abs(Weights[i].Values[^1])}");
            
            functionsStrings.Add(builder.ToString());
        }

        return functionsStrings;
    }

    public void PrintFunctionsStrings()
    {
        var functionsStrings = GenerateFunctionsStrings();
        foreach (var functionString in functionsStrings)
        {
            Console.WriteLine(functionString);
        }
    }
    
    private int MultiplyVectors(List<int> a, List<int> b)
    {
        if (a.Count != b.Count)
        {
            throw new ArgumentException();
        }

        var result = 0;
        
        for (var i = 0; i < a.Count; i++)
        {
            result += a[i] * b[i];
        }

        return result;
    }

    private List<int> AddVector(List<int>  a, List<int>  b)
    {
        if (a.Count != b.Count)
        {
            throw new ArgumentException();
        }

        var result = new List<int>();
        
        for (var i = 0; i < a.Count; i++)
        {
            result.Add(a[i] + b[i]);
        }

        return result;
    }
    
    private List<int> SubVector(List<int> a, List<int> b)
    {
        if (a.Count != b.Count)
        {
            throw new ArgumentException();
        }

        var result = new List<int>();
        
        for (var i = 0; i < a.Count; i++)
        {
            result.Add(a[i] - b[i]);
        }

        return result;
    }
    
    private void GenerateClasses(int classesCount, int objectsCount, int attributesCount)
    {
        for (var i = 0; i < classesCount; i++)
        {
            Classes.Add(new Class(objectsCount, attributesCount, _min, _max));
        }
    }

    private void InitWeights(int amount, int size)
    {
        for (var i = 0; i < amount; i++)
        {
            Weights.Add(new Weight(size));
        }
    }
}
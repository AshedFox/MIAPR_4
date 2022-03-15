using System.Security.Cryptography;

namespace Lab4.Entity;

public class Object
{
    public List<int> Attributes { get; set; } = new ();

    public Object() { }
    
    public Object(int attributesCount, int min, int max)
    {
        GenerateAttributes(attributesCount, min, max);
    }
    
    private void GenerateAttributes(int attributesCount, int min, int max)
    {
        for (var i = 0; i < attributesCount; i++)
        {
            Attributes.Add(RandomNumberGenerator.GetInt32(min, max));
        }
        Attributes.Add(1);
    }
}
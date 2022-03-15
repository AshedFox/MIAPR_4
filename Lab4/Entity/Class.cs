namespace Lab4.Entity;

public class Class
{
    public List<Object> Objects { get; set; } = new();

    public Class() { }
    
    public Class(int objectsCount, int attributesCount, int min, int max)
    {
        GenerateObjects(objectsCount, attributesCount, min, max);
    }

    private void GenerateObjects(int objectsCount, int attributesCount, int min, int max)
    {
        for (var i = 0; i < objectsCount; i++)
        {
            Objects.Add(new Object(attributesCount, min, max));
        }
    }
}
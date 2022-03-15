namespace Lab4.Entity;

public class Weight
{
    public List<int> Values { get; set; } = new();

    public Weight(int size)
    {
        InitWeight(size);
    }

    private void InitWeight(int size)
    {
        Values.AddRange(new int [size]);
    }
}
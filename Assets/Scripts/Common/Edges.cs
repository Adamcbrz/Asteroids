/// <summary>
/// Edges defines the Ranges for the EcuclideanTorus
/// </summary>
public struct Edges {
    
    public Range x;
    public Range y;

    public override string ToString()
    {
        return string.Format("[Edge] (x: {0}), (y: {1})", x.ToString(), y.ToString());
    }
}

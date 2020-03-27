[System.Serializable]
public struct Range
{
    public float min;
    public float max;

    public float Random()
    {
        return UnityEngine.Random.Range(min, max);
    }

    public override string ToString()
    {
        return string.Format("[Range] ({0}, {1})", min, max);
    }
}

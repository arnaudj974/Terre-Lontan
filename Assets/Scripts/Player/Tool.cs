public class Tool
{
    public enum Type { Pioche, Hache, Houe, Mains, Arros};
    public Type type { get; private set; }
    public int index { get; private set; }

    public Tool(Type type)
    {
        this.type = type;
        this.index = type switch
        {
            Type.Pioche => 1,
            Type.Hache => 2,
            Type.Houe => 3,
            Type.Mains => 4,
            Type.Arros => 5,
            _=>4
        };
    }

    public Tool(int i)
    {
        this.index = i;
        this.type = i switch
        {
            1 => Type.Pioche,
            2 => Type.Hache,
            3 => Type.Houe,
            4 => Type.Mains,
            5 => Type.Arros,
            _=>Type.Mains
        };
    }

    public bool Same(Tool tool)
    {
        return this.type == tool.type;
    }
}

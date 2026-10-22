using UnityEngine;

public class TypeCase
{
    public enum Type { Arbre, Rocher, Herbe, Plantable, Plante };
    public Type type { get; private set; }
    public Tool tool { get; private set; }
    public bool canArros { get; private set; }
    public HarvestablePlant plant { get; private set; }
    public Sprite sprite { get; private set; }
    public TypeCase(Type type,Sprite sprite)
    {
        canArros = type == Type.Plante;
        this.type = type;
        this.sprite = sprite;
        this.tool = type switch
        {
            Type.Arbre => new Tool(Tool.Type.Hache),
            Type.Rocher => new Tool(Tool.Type.Pioche),
            Type.Herbe => new Tool(Tool.Type.Houe),
            Type.Plantable or Type.Plante => new Tool(Tool.Type.Mains),
        };
    }
    public TypeCase(HarvestablePlant plant)
    {
        this.canArros = true;
        this.tool = new Tool(Tool.Type.Mains);
        this.type = Type.Plante;
        this.plant = plant;
    }

    public bool CanUse(Tool tool)
    {
        return this.tool == tool;
    }

    public string NomType()
    {
        return type.ToString();
    }
}

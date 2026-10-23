using UnityEngine;
using UnityEngine.Tilemaps;

public class TypeCase
{
    public GameObject instance;
    public enum Type { Arbre, Rocher, Herbe, Terre, Plante };
    public Type type { get; private set; }
    public Tool tool { get; private set; }
    public bool canArros { get; private set; }
    //public HarvestablePlant plant { get; private set; }
    public Sprite sprite { get; private set; }
    public TileBase tile { get; private set; }
    public bool isTile { get; private set; }
    public bool isSprite { get; private set; }
    public string name;
    public TypeCase(Type type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
        this.isSprite = true;
        this.tool = type switch
        {
            Type.Arbre => new Tool(Tool.Type.Hache),
            Type.Rocher => new Tool(Tool.Type.Pioche),
            Type.Herbe => new Tool(Tool.Type.Houe),
            Type.Terre or Type.Plante => new Tool(Tool.Type.Mains),
            _=>null
        };
    }

    public TypeCase(Type type, TileBase tile)
    {
        this.type = type;
        this.tile = tile;
        this.isTile = true;
        this.tool = type switch
        {
            Type.Arbre => new Tool(Tool.Type.Hache),
            Type.Rocher => new Tool(Tool.Type.Pioche),
            Type.Herbe => new Tool(Tool.Type.Houe),
            Type.Terre or Type.Plante => new Tool(Tool.Type.Mains),
            _=>null
        };
    }

    public TypeCase(GameObject plant,string name)
    {
        this.canArros = true;
        this.tool = new Tool(Tool.Type.Mains);
        this.type = Type.Plante;
        this.instance = plant;
        this.name = name;
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

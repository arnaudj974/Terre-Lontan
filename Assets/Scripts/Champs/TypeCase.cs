using UnityEngine;
using UnityEngine.Tilemaps;

public class TypeCase
{
    
    public enum Type { Arbre, Rocher, Herbe, Terre, Plante };
    public Type type { get; private set; }
    public Tool tool { get; private set; }
    public bool canArros { get; private set; }
    public Sprite sprite { get; private set; }
    public TileBase tile { get; private set; }
    public GameObject plante;
    public string name;
    public TypeCase(Type type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
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
        this.tool = type switch
        {
            Type.Arbre => new Tool(Tool.Type.Hache),
            Type.Rocher => new Tool(Tool.Type.Pioche),
            Type.Herbe => new Tool(Tool.Type.Houe),
            Type.Terre or Type.Plante => new Tool(Tool.Type.Mains),
            _=>null
        };
    }

    public TypeCase(GameObject plante,string name)
    {
        this.canArros = true;
        this.tool = new Tool(Tool.Type.Mains);
        this.type = Type.Plante;
        this.plante = plante;
        this.name = name;
    }

    public TypeCase Evolution(Type type, TileBase tileTerre,TileBase tileHerbe,GameObject plant,string nom)
    {
        return type switch
        {
            Type.Arbre or Type.Rocher => new TypeCase(Type.Herbe, tileHerbe),
            Type.Herbe => new TypeCase(Type.Terre, tileTerre),
            Type.Terre => new TypeCase(plant, nom),
            _ => null
        }; 

    }

    public bool CanUse(Tool tool)
    {
        return this.tool == tool;
    }

    public string NomType()
    {
        return type.ToString();
    }
    public float GetEnergy()
    {
        return this.type switch
        {
            Type.Arbre or Type.Rocher => 5f,
            Type.Herbe or Type.Terre => 3f,
            Type.Plante => 2f,
            _=>2
        };
    }

    public bool IsSprite()
    {
        return sprite != null;
    }
    public bool IsTile()
    {
        return tile != null;
    }
}

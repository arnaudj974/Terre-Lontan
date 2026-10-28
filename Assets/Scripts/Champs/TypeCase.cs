using UnityEngine;
using UnityEngine.Tilemaps;

public class TypeCase
{
   // private BiblioCase biblioSprite;
    private BiblioTile biblioTile;
    public enum Type { Arbre, Rocher, Herbe, Terre, Plante };
    public Type type { get; private set; }
    public Tool tool { get; private set; }
    public Sprite sprite { get; private set; }
    public TileBase tile { get; private set; }
    public GameObject plante;
    public string name;
    public TypeCase(Type type, BiblioCase biblioSprite,BiblioTile biblioTile,string name)
    {
        this.biblioTile = biblioTile;
        this.type = type;
        this.sprite = name != null ? biblioSprite.ObtenirRand(type.ToString()) : biblioSprite.Obtenir(name);
        this.tile = biblioTile.ObtenirRand("Herbe");
        this.name = biblioSprite.Obtenir(this.sprite);
        this.tool = type switch
        {
            Type.Arbre => new Tool(Tool.Type.Hache),
            Type.Rocher => new Tool(Tool.Type.Pioche),
            Type.Herbe => new Tool(Tool.Type.Houe),
            Type.Terre or Type.Plante => new Tool(Tool.Type.Mains),
            _=>null
        };
    }

    public TypeCase(Type type,  BiblioTile biblioTile)
    {
        this.biblioTile = biblioTile;
        this.type = type;
        this.tile = biblioTile.ObtenirRand(type.ToString());
        this.tool = type switch
        {
            Type.Arbre => new Tool(Tool.Type.Hache),
            Type.Rocher => new Tool(Tool.Type.Pioche),
            Type.Herbe => new Tool(Tool.Type.Houe),
            Type.Terre or Type.Plante => new Tool(Tool.Type.Mains),
            _=>null
        };
    }

    public TypeCase(GameObject plante,string name, BiblioTile biblioTile)
    {
        this.biblioTile = biblioTile;
        this.tile= biblioTile.ObtenirRand("Terre");
        this.tool = new Tool(Tool.Type.Mains);
        this.type = Type.Plante;
        this.plante = plante;
        this.name = name;
    }

    public TypeCase Evolution(Type type,  GameObject plant, string nom, BiblioCase biblioSprite, BiblioTile biblioTile)
    {
        return type switch
        {
            Type.Arbre or Type.Rocher => new TypeCase(Type.Herbe, biblioTile),
            Type.Herbe => new TypeCase(Type.Terre,  biblioTile),
            Type.Terre => new TypeCase(plant, nom, biblioTile),
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
            _=>2f
        };
    }

    public bool IsSprite()
    {
        return (type == Type.Arbre || type == Type.Rocher);
    }
    public bool IsTile()
    {
        return (type == Type.Herbe || type == Type.Terre);
    }

    public void IsNight()
    {
        if (this.type == Type.Plante) this.plante.GetComponent<HarvestablePlant>().IsNight();
        if (this.type == Type.Terre)
        {
            this.type = Type.Herbe;
            this.tool = new Tool(Tool.Type.Houe);
            this.tile = this.biblioTile.ObtenirRand("Herbe");
        }
    }
}

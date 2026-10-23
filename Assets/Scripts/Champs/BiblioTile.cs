using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BiblioTile", menuName = "Scripting/BiblioTile")]
public class BiblioTile : ScriptableObject
{
    [System.Serializable]
    public struct Entree
    {
        public string nom;
        public TileBase tile;
    }

    public Entree[] entrees;

    public TileBase Obtenir(string nom)
    {
        foreach (var e in entrees)
            if (e.nom == nom) return e.tile;

        Debug.LogWarning($"Tile introuvable : {nom}");
        return null;
    }
    public TileBase ObtenirRand(string nom)
    {
        int random = Random.Range(0, 3);
        nom += random.ToString();
        foreach (var e in entrees)
            if (e.nom == nom) return e.tile;

        Debug.LogWarning($"Tile introuvable : {nom}");
        return null;
    }
}

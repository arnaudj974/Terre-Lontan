using UnityEngine;

[CreateAssetMenu(fileName = "BiblioCase", menuName = "Scripting/BiblioCase")]
public class BiblioCase : ScriptableObject
{
    [System.Serializable]
    public struct Entree
    {
        public string nom;
        public Sprite sprite;
    }

    public Entree[] entrees;

    public Sprite Obtenir(string nom)
    {
        foreach (var e in entrees)
            if (e.nom == nom) return e.sprite;

        Debug.LogWarning($"Sprite introuvable : {nom}");
        return null;
    }

    public Sprite ObtenirRand(string nom)
    {
        int random = Random.Range(0, 3);
        nom += random.ToString();
        foreach (var e in entrees)
            if (e.nom == nom) return e.sprite;

        Debug.LogWarning($"Sprite introuvable : {nom}");
        return null;
    }

    public string Obtenir(Sprite s)
    {
        foreach (var e in entrees)
            if (e.sprite == s) return e.nom;
        Debug.LogWarning("Sprite introuvable");
        return null;
    }

}

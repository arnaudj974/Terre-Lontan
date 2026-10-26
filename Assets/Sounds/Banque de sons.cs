using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Banque de sons")]
public class BanqueDeSons : ScriptableObject
{
    [System.Serializable]
    public struct Entree
    {
        public string nom;
        public AudioClip clip;
    }

    public Entree[] entrees;

    public AudioClip Obtenir(string nom)
    {
        foreach (var e in entrees)
            if (e.nom == nom) return e.clip;

        Debug.LogWarning($"Son introuvable : {nom}");
        return null;
    }
}
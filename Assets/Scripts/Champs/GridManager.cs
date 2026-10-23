using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridManager : MonoBehaviour
{
    [SerializeField] private BiblioCase banque;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tile;

    [Header("Grille")]
    [SerializeField] private Vector2Int size = new Vector2Int(10, 8); 
    public float cellSize = 1f;

    [Header("Placement")]
    [SerializeField] private GameObject prefab;

    // Cases occupées : coordonnée de la case -> objet posé dessus
    private readonly Dictionary<Vector2Int, TypeCase> occupied = new();

    // ---------- Conversions ----------

    // Coin bas-gauche de la grille, la grille est centrée sur ce GameObject
    Vector3 Origin => transform.position - new Vector3(size.x, size.y, 0) * cellSize / 2f;
    public bool IsFree(Vector2Int cell) => IsInside(cell) && !occupied.ContainsKey(cell);

    public Vector2Int WorldToCell(Vector3 world)
    {
        Vector3 local = world - Origin;
        return new Vector2Int(Mathf.FloorToInt(local.x / cellSize),Mathf.FloorToInt(local.y / cellSize));
    }
    public Vector3 CellToWorld(Vector2Int cell)
    {
        // Centre de la case
        return Origin + new Vector3( (cell.x + 0.5f) * cellSize, (cell.y + 0.5f) * cellSize, 0);
    }

    public bool IsInside(Vector2Int cell) => cell.x >= 0 && cell.y >= 0 && cell.x < size.x && cell.y < size.y;


    void Awake()
    {
        if (occupied.Count != 0) return; //Load ?
        CreateWorld();
    }


    // ---------- Placement ----------

    void CreateWorld()
    {
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                switch (Random.Range(0, 6))
                {
                    case 0: Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Arbre, banque.Obtenir("Arbre"+Random.Range(0,3))));
                        break;
                    case 1: Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Rocher, banque.Obtenir("Rocher" + Random.Range(0, 3))));
                        break;
                    default: break;
                }
            }
        }
    }


    public void Place(Vector2Int cell, TypeCase obj)
    {
        if (!IsFree(cell)) return;
        occupied[cell] = obj;
        GameObject temp = new GameObject("PlacedSprite");
        temp.transform.SetParent(transform);
        temp.transform.position = CellToWorld(cell);

        SpriteRenderer sr = temp.AddComponent<SpriteRenderer>();
        sr.sprite = obj.sprite;
        sr.sortingOrder = -Mathf.RoundToInt(CellToWorld(cell).y * 10);
    }
    public void PlacerTerre(Vector2Int cell)
    {
        tilemap.SetTile(new Vector3Int(cell.x, cell.y, 0), tile);
    }
    /* public bool Place(Vector2Int cell, TypeCase obj)
      {
          if (!IsInside(cell) || occupied.ContainsKey(cell)) return false;

          GameObject obj = Instantiate(prefab, CellToWorld(cell), Quaternion.identity, transform);
          occupied[cell] = obj;
          return true;
      }

      public void Remove(Vector2Int cell)
      {
          if (!occupied.TryGetValue(cell, out GameObject obj)) return;
          Destroy(obj);
          occupied.Remove(cell);
      }*/

    // ---------- Affichage dans la Scene ----------

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.4f);

        // Lignes de la grille
        for (int x = 0; x <= size.x; x++)
            Gizmos.DrawLine(Origin + new Vector3(x * cellSize, 0, 0), Origin + new Vector3(x * cellSize, size.y * cellSize, 0));
        for (int y = 0; y <= size.y; y++)
            Gizmos.DrawLine(Origin + new Vector3(0, y * cellSize, 0), Origin + new Vector3(size.x * cellSize, y * cellSize, 0));

        // Contour de la zone
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x * cellSize, size.y * cellSize, 0.1f));
    }
}

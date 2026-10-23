using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject plantPrefab;
    [SerializeField] BiblioCase banqueCase;
    [SerializeField] BiblioTile banqueTile;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Vector2Int size = new Vector2Int(10, 8);
    [SerializeField] float timeRemove = 3f;
    public float cellSize = 1f;
    Coroutine coroutReplace;
    private readonly Dictionary<Vector2Int, TypeCase> occupied = new();

    Vector3 Origin => tilemap.CellToWorld(Vector3Int.zero);
    public bool IsFree(Vector2Int cell) => IsInside(cell) && !occupied.ContainsKey(cell);

    public Vector2Int WorldToCell(Vector3 world)
    {
        Vector3 local = world - Origin;
        return new Vector2Int(Mathf.FloorToInt(local.x / cellSize), Mathf.FloorToInt(local.y / cellSize));
    }
    public Vector3 CellToWorld(Vector2Int cell)
    {
        // Centre de la case
        return Origin + new Vector3((cell.x + 0.5f) * cellSize, (cell.y + 0.5f) * cellSize, 0);
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
                Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Herbe, banqueTile.ObtenirRand("Herbe")));
                switch (Random.Range(0, 6))
                {
                    case 0:
                        Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Arbre, banqueCase.ObtenirRand("Arbre")));
                        break;
                    case 1:
                        Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Rocher, banqueCase.ObtenirRand("Rocher")));
                        break;
                    default: break;
                }
            }
        }
    }


    public void Place(Vector2Int cell, TypeCase obj)
    {

        if (obj.isSprite)
        {
            GameObject temp = new GameObject("PlacedSprite");
            temp.transform.SetParent(transform);
            temp.transform.position = CellToWorld(cell);
            SpriteRenderer sr = temp.AddComponent<SpriteRenderer>();
            sr.sprite = obj.sprite;
            sr.sortingOrder = -Mathf.RoundToInt(CellToWorld(cell).y * 10);
            obj.instance = temp;
        }
        else if (obj.isTile)
        {
            PlacerTile(cell, obj.tile);
        }
        else
        {
            Debug.Log("plante");
            GameObject temp = new GameObject("PlacedPlant");
            obj.instance.GetComponent<HarvestablePlant>().SetPlantType(obj.name);
            temp = Instantiate(obj.instance, CellToWorld(cell), Quaternion.identity, transform);
        }
        occupied[cell] = obj;
    }

    public void PlacerTile(Vector2Int cell, TileBase tile)
    {
        tilemap.SetTile(new Vector3Int(cell.x, cell.y, 0), tile);
    }


    IEnumerator Replace(Vector2Int cell, TypeCase obj,string name)
    {
        yield return new WaitForSeconds(timeRemove);
        if (obj.instance != null && obj.isSprite) //supprime le gameobject et mets une tile d'herbe
        {
            Destroy(obj.instance);
            Place(cell, new TypeCase(TypeCase.Type.Herbe, banqueTile.ObtenirRand("Herbe")));
        }
        if (obj.type == TypeCase.Type.Herbe) Place(cell, new TypeCase(TypeCase.Type.Terre, banqueTile.ObtenirRand("Terre"))); // met de la terre à la place de l'herbe
        if (obj.type == TypeCase.Type.Terre) Place(cell, new TypeCase(plantPrefab,name));
    }

    public void StopAttack()
    {
        StopCoroutine(coroutReplace);
    }

    public void ChangeCell(Vector2Int cell, Tool tool,string plante)
    {
        if (!occupied.TryGetValue(cell, out TypeCase obj)) return;
        if (obj.tool.Same(tool)) // si le joueur a le bon outil
        {
            coroutReplace = StartCoroutine(Replace(cell, obj, plante));
        }
    }

    void OnDrawGizmos()
    {
        Vector3 origin = Origin;
        Vector3 dimensions = new Vector3(size.x * cellSize, size.y * cellSize, 0f);

        // Lignes de la grille
        Gizmos.color = new Color(1, 1, 1, 0.4f);
        for (int x = 0; x <= size.x; x++)
            Gizmos.DrawLine(origin + new Vector3(x * cellSize, 0, 0),
                            origin + new Vector3(x * cellSize, dimensions.y, 0));
        for (int y = 0; y <= size.y; y++)
            Gizmos.DrawLine(origin + new Vector3(0, y * cellSize, 0),
                            origin + new Vector3(dimensions.x, y * cellSize, 0));

        // Contour de la zone : centre = origine + moitié des dimensions
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(origin + dimensions / 2f, new Vector3(dimensions.x, dimensions.y, 0.1f));
    }
}

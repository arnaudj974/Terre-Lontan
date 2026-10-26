using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    bool isCoroutReplaceStarted;
    Coroutine coroutArrose;
    bool isCoroutArroseStarted;
    private readonly Dictionary<Vector2Int, TypeCase> occupied = new();
    public UnityEvent<float> OnReplace;

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
    void Start()
    {
        WorldTime.instance.Sunset += IsNight;
    }


    // ---------- Placement ----------

    void CreateWorld()
    {
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Herbe,banqueTile));
                switch (Random.Range(0, 6))
                {
                    case 0:
                        Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Arbre,banqueCase,banqueTile));
                        break;
                    case 1:
                        Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Rocher, banqueCase, banqueTile));
                        break;
                    default: break;
                }
            }
        }
    }

    public void PlaceTile(Vector2Int cell, TypeCase obj)
    {
        tilemap.SetTile(new Vector3Int(cell.x, cell.y, 0), obj.tile);
    }
    public void ColorerTile(Vector2Int cell, Color color)
    {
        Vector3Int pos = new Vector3Int(cell.x, cell.y, 0);

        tilemap.SetTileFlags(pos, TileFlags.None);   // déverrouille la couleur
        tilemap.SetColor(pos, color);
    }
    public void Place(Vector2Int cell, TypeCase obj)
    {
        PlaceTile(cell, obj);
        if (obj.IsSprite())
        {
            GameObject temp = new GameObject("PlacedSprite");
            temp.transform.SetParent(transform);
            temp.transform.position = CellToWorld(cell);
            SpriteRenderer sr = temp.AddComponent<SpriteRenderer>();
            sr.sprite = obj.sprite;
            sr.sortingOrder = -Mathf.RoundToInt(CellToWorld(cell).y * 10);
            obj.plante = temp;
        }
        else if (obj.type == TypeCase.Type.Plante)
        {
            GameObject temp = new GameObject("PlacedPlant");
            obj.plante.GetComponent<HarvestablePlant>().SetPlantType(obj.name); //créer la nouvelle plante
            obj.plante.GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(CellToWorld(cell).y * 10);
            temp = Instantiate(obj.plante, CellToWorld(cell), Quaternion.identity, transform);
            obj.plante = temp;
        }
        occupied[cell] = obj;
    }


    IEnumerator Replace(Vector2Int cell, TypeCase oldObj,TypeCase newObj)
    {
        isCoroutReplaceStarted = true;
        yield return new WaitForSeconds(timeRemove);
        if (oldObj.IsSprite()) //supprime le gameobject et mets une tile d'herbe
        {
            Destroy(oldObj.plante);
        }
        Place(cell, newObj);

        OnReplace?.Invoke(oldObj.GetEnergy());
        isCoroutReplaceStarted = false;
    }

    public void StopAttack()
    {
        if(isCoroutReplaceStarted) StopCoroutine(coroutReplace);
        if (isCoroutArroseStarted) StopCoroutine(coroutArrose);
    }

    public void ChangeCell(Vector2Int cell, Tool tool, string nomPlante)
    {
        if (!occupied.TryGetValue(cell, out TypeCase obj)) return;
        if (obj.tool.Same(tool)) // si le joueur a le bon outil
        {

            TypeCase newObj = obj.Evolution(obj.type, plantPrefab, nomPlante,banqueCase,banqueTile); // créer la nouvelle typecase qui va remplacer l'ancienne
            coroutReplace = StartCoroutine(Replace(cell, obj, newObj));
        }
        if (tool.type == Tool.Type.Arros && obj.type == TypeCase.Type.Plante)// si le player à l'arros et c'est une plante sur la case 
        {
            coroutArrose = StartCoroutine(Arroser(cell,obj));
        }
    }

    IEnumerator Arroser(Vector2Int cell, TypeCase obj)
    {
        isCoroutArroseStarted = true;
        yield return new WaitForSeconds(timeRemove);
        obj.plante.GetComponent<HarvestablePlant>().Watered();
        ColorerTile(cell, Color.brown);
        isCoroutArroseStarted = false;
    }

    public void IsNight()
    {
        foreach (var item in occupied)
        {
            item.Value.IsNight();
            PlaceTile(item.Key, item.Value);
            ColorerTile(item.Key, Color.white);
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

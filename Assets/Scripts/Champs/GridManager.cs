using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] BanqueDeSons BanqueSons;
    [SerializeField] GameObject plantPrefab;
    [SerializeField] GameObject fruitPrefab;
    [SerializeField] BiblioCase banqueCase;
    [SerializeField] BiblioTile banqueTile;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Vector2Int size = new Vector2Int(10, 8);
    [SerializeField] float timeRemove = 3f;
    public float cellSize = 1f;
    Coroutine corout;
    bool isCoroutStarted;
    private Dictionary<Vector2Int, TypeCase> occupied = new();
    public UnityEvent<float> OnWork;
    public UnityEvent<float> Onarros;
    AudioSource audioS;

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
        audioS=GetComponent<AudioSource>();
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
                switch (Random.Range(0, 6))
                {
                    case 0:
                        Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Arbre, banqueCase, banqueTile));
                        break;
                    case 1:
                        Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Rocher, banqueCase, banqueTile));
                        break;
                    default: Place(new Vector2Int(j, i), new TypeCase(TypeCase.Type.Herbe, banqueTile)); break;
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
            obj.plante.GetComponent<HarvestablePlant>().Planted();
            obj.plante.GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(CellToWorld(cell).y * 10);
            temp = Instantiate(obj.plante, CellToWorld(cell), Quaternion.identity, transform);
            obj.plante = temp;
        }
        occupied[cell] = obj;
    }

    public void StopAttack()
    {
        audioS.Stop();
        if (isCoroutStarted)
        {
            StopCoroutine(corout);
            isCoroutStarted = false;
        }
    }

    public void ChangeCell(Vector2Int cell, Tool tool, string nomPlante)
    {
        if (!occupied.TryGetValue(cell, out TypeCase obj)) return;
        if (obj.tool.Same(tool)) // si le joueur a le bon outil
        {
            if (obj.type != TypeCase.Type.Plante)
            {
                TypeCase newObj = obj.Evolution(obj.type, plantPrefab, nomPlante, banqueCase, banqueTile); // créer la nouvelle typecase qui va remplacer l'ancienne
                corout = StartCoroutine(Replace(cell, obj, newObj));
            }
            else if (obj.plante.GetComponent<HarvestablePlant>().harvest == true)
            {
                corout = StartCoroutine(Recolter(cell, obj));
            }
        }
        else
            if (tool.type == Tool.Type.Arros && obj.type == TypeCase.Type.Plante)// si le player à l'arros et c'est une plante sur la case 
            {
                corout = StartCoroutine(Arroser(cell, obj));
            }
            else PlaySound("Fail");
    }
    IEnumerator Replace(Vector2Int cell, TypeCase oldObj, TypeCase newObj)
    {
        isCoroutStarted = true;
        switch (oldObj.type)
        {
            case TypeCase.Type.Arbre: PlaySound("Hache"); break;
            case TypeCase.Type.Rocher: PlaySound("Pioche"); break;
            case TypeCase.Type.Herbe: PlaySound("Houe"); break;
            case TypeCase.Type.Terre: PlaySound("Planter"); break;
        }
        yield return new WaitForSeconds(timeRemove);
        if (oldObj.IsSprite()) //supprime le gameobject et mets une tile d'herbe
        {
            Destroy(oldObj.plante);
        }
        Place(cell, newObj);

        OnWork?.Invoke(oldObj.GetEnergy());
        isCoroutStarted = false;
    }

    IEnumerator Arroser(Vector2Int cell, TypeCase obj)
    {
        isCoroutStarted = true;
        PlaySound("Arros");
        yield return new WaitForSeconds(timeRemove);
        obj.plante.GetComponent<HarvestablePlant>().Watered();
        ColorerTile(cell, Color.brown);
        OnWork?.Invoke(obj.GetEnergy());
        Onarros?.Invoke(1f);
        isCoroutStarted = false;
    }

    IEnumerator Recolter(Vector2Int cell, TypeCase obj)
    {
        isCoroutStarted = true;
        PlaySound("Recolter");
        yield return new WaitForSeconds(timeRemove);

        GameObject temp = new GameObject("PlacedFruit");
        HarvestablePlant plant = obj.plante.GetComponent<HarvestablePlant>();
        fruitPrefab.GetComponent<HarvestablePlant>().SetPlantType(plant.GetName());
        fruitPrefab.GetComponent<SpriteRenderer>().sprite = plant.GetSpriteFruit();
        fruitPrefab.GetComponent<Image>().sprite = plant.GetSpriteFruit();
        temp = Instantiate(fruitPrefab, CellToWorld(cell+Vector2Int.right), Quaternion.identity, transform);

        obj.plante.GetComponent<HarvestablePlant>().Harvest();
        OnWork?.Invoke(obj.GetEnergy());
        isCoroutStarted = false;
    }

    public void IsNight()
    {
        audioS.Stop();
        foreach (var item in occupied)
        {
            item.Value.IsNight();
            PlaceTile(item.Key, item.Value);
            ColorerTile(item.Key, Color.white);
        }
    }

    void PlaySound(string son)
    {
        audioS.clip=BanqueSons.Obtenir(son);
        audioS.Play();
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

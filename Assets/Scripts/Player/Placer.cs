using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Placer : MonoBehaviour
{
    [SerializeField] private GridManager grid;
    [SerializeField] private Transform feet;
    [SerializeField] private SpriteRenderer highlight;
    [SerializeField] private Color validColor = new Color(0f, 1f, 0f, 0.35f);
    [SerializeField] private Color invalidColor = new Color(1f, 0f, 0f, 0.35f);
    Vector2Int targetCell;
    PlayerAnimation playerAnim;
    public UnityEvent<bool> OnAttack;

    

    void Awake()
    {
        playerAnim = GetComponent<PlayerAnimation>();
        highlight.transform.localScale = Vector3.one * grid.cellSize;
    }
    void Update()
    {
        targetCell = grid.WorldToCell(feet.position);
        UpdateHighlight();
        if (grid.IsInside(targetCell)) playerAnim.SetOrder(-Mathf.RoundToInt(feet.position.y * 10)); //changer l'order du spriterender
        else playerAnim.SetOrder(1000);
    }
    void UpdateHighlight()
    {
        bool inside = grid.IsInside(targetCell);
        highlight.enabled = inside;
        if (!inside) return;

        highlight.transform.position = grid.CellToWorld(targetCell);
        highlight.color = grid.IsFree(targetCell) ? validColor : invalidColor;
    }

  /*  void OnDrawGizmos()
    {
        if (grid == null || !Application.isPlaying) return;

        Gizmos.color = grid.IsInside(targetCell)
            ? new Color(0, 1, 0, 0.4f)
            : new Color(1, 0, 0, 0.4f);

        Gizmos.DrawCube(grid.CellToWorld(targetCell),
            new Vector3(grid.cellSize, grid.cellSize, 0.1f));
    }*/


    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            OnAttack?.Invoke(true);
        }
        else if (ctx.canceled) OnAttack?.Invoke(false);
    }
}
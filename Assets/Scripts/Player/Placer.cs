using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEditor.Tilemaps;

public class Placer : MonoBehaviour
{
    [SerializeField] private GridManager grid;
    [SerializeField] private Transform feet;
    [SerializeField] private SpriteRenderer highlight;
   // [SerializeField] private Color validColor = new Color(0f, 1f, 0f, 0.35f);
    //[SerializeField] private Color invalidColor = new Color(1f, 0f, 0f, 0.35f);
    Vector2Int targetCell;
    PlayerAnimation playerAnim;
    PlayerController playerCont;
    Player player;
    public UnityEvent<bool> OnAttack;

    

    void Awake()
    {
        player = GetComponent<Player>();
        playerCont = GetComponent<PlayerController>();
        playerAnim = GetComponent<PlayerAnimation>();
        highlight.transform.localScale = Vector3.one * grid.cellSize;
    }
    void Update()
    {
        targetCell = grid.WorldToCell(feet.position);
        UpdateHighlight(playerCont.moveDir == Vector2.zero && grid.IsInside(targetCell)); //mets les hightlight si le player ne bouge pas et qu'il est dans la grille
        if (grid.IsInside(targetCell)) playerAnim.SetOrder(-Mathf.RoundToInt(feet.position.y * 10)); //changer l'order du spriterender du player
        else playerAnim.SetOrder(1000);
    }
    void UpdateHighlight(bool update)
    {
        bool inside = grid.IsInside(targetCell);
        highlight.enabled = update;
        if (!inside) return;

        highlight.transform.position = grid.CellToWorld(targetCell);
        //highlight.color = grid.IsFree(targetCell) ? validColor : invalidColor;
    }

    void Attack()
    {
        if (grid.IsInside(targetCell))
        {
            grid.ChangeCell(targetCell, player.tool);
        }
    }

    void StopAttack()
    {
        if (grid.IsInside(targetCell))
        {
            grid.StopAttack();
        }
    }
    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            OnAttack?.Invoke(true);
            Attack();
        }
        else if (ctx.canceled)
        {
            OnAttack?.Invoke(false);
            StopAttack();
        }
    }
}
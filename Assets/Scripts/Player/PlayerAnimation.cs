using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerController playerController;
    Player player;
    SpriteRenderer sr;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        player = GetComponent<Player>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleAnimVariable();
    }

    void HandleAnimVariable()
    {
        bool isMoving = playerController.moveDir != Vector2.zero;
        anim.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            anim.SetFloat("moveX", playerController.LastDir.x);
            anim.SetFloat("moveY", playerController.LastDir.y);
        }
        anim.SetFloat("tool", player.tool.index);
        anim.SetBool("attack", playerController.isAttacking);
    }
    public void SetOrder(int i)
    {
        sr.sortingOrder = i;
    }
}

using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerController playerController;
    Player player;
    bool isattackingTemp;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        player = GetComponent<Player>();
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
        anim.SetFloat("tool", player.tool);
        anim.SetBool("attack", playerController.isAttacking);
    }

}

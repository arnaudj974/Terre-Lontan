
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
     Animator anim;
     PlayerController playerController;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
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
            Vector2 dir;

            if (playerController.moveDir.x != 0)
                dir = new Vector2(Mathf.Sign(playerController.moveDir.x), 0);   // horizontal prioritaire
            else
                dir = new Vector2(0, Mathf.Sign(playerController.moveDir.y));   // sinon vertical

            anim.SetFloat("moveX", dir.x);
            anim.SetFloat("moveY", dir.y);
        }
    }

}


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
            anim.SetFloat("moveX", playerController.LastDir.x);
            anim.SetFloat("moveY", playerController.LastDir.y);
        }
    }

}

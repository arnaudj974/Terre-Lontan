using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{ 
    [SerializeField] PlayerInput playerInput;
    public GameObject menuCanvas;

    void Start()
    {
        menuCanvas.SetActive(false);
    }
    public void MenuPlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            menuCanvas.SetActive(true);
            playerInput.SwitchCurrentActionMap("UI");
        }
    }
    public void MenuUI(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            menuCanvas.SetActive(false);
            playerInput.SwitchCurrentActionMap("Player");
        }
    }
}

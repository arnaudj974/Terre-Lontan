using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject panelWheel;
    [SerializeField] Image imgWheel;
    [SerializeField] Sprite spriteWheelPioche;
    [SerializeField] Sprite spriteWheelHache;
    [SerializeField] Sprite spriteWheelHoue;
    [SerializeField] Sprite spriteWheelMains;
    [SerializeField] Sprite spriteWheelArros;
    public UnityEvent<int> OnWheelChange;
    bool wheelHeld;

    void Awake()
    {
        panelWheel.SetActive(false);
    }

    void Update()
    {
        if (wheelHeld) ChangeSpriteWheel(ToolWithAngle());
    }

    float Angle()
    {
        //  Récupérer la position via le New Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // 3. Calculer la direction et l'angle
        Vector2 direction = mousePos - screenCenter;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        if (angle < 0) angle += 360f;
        return angle;
    }

    int ToolWithAngle()
    {

        float angle = Angle();
        if ((angle >= 0 && angle < 36) || angle <= 360 && angle > 324)
        {
            return 1;
        }
        if (angle >= 36 && angle < 108)
        {
            return 2;
        }
        if (angle >= 108 && angle < 180)
        {
            return 3;
        }
        if (angle >= 180 && angle < 252)
        {
            return 4;
        }
        if (angle >= 252 && angle <= 324)
        {
            return 5;
        }
        return 0;
    }

    void ChangeSpriteWheel(int ind)
    {
        OnWheelChange?.Invoke(ind);
        imgWheel.sprite = ind switch
        {
            1 => spriteWheelPioche,
            2 => spriteWheelHache,
            3 => spriteWheelHoue,
            4 => spriteWheelMains,
            5 => spriteWheelArros,
            _ => null
        };
    }

    public void Wheel(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            wheelHeld = true;
            panelWheel.SetActive(true);
        }
        else if (ctx.canceled)
        {
            wheelHeld = false;
            panelWheel.SetActive(false);
        }
    }
}

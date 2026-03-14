using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    InputAction clickAction;
    InputAction pointAction;
    public Vector2 mousePos_screen {get; set;}
    public Vector3 mousePos_world { get; set; }

    void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
        pointAction = InputSystem.actions.FindAction("Point");
    }

    void Update()
    {
        mousePos_screen = pointAction.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos_screen);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mousePos_world = hit.point;
        }
    }

    void OnClick()
    {
        if(!clickAction.IsPressed())
        {
            return;
        }
        Debug.Log($"마우스 좌표 : {mousePos_world}" );
        GameManager.gameManager.ClickMouse(mousePos_world);
        
    }

}

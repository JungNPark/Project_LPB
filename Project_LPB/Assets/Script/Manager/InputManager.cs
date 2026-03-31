using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    InputAction clickAction;
    InputAction pointAction;

    void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
        pointAction = InputSystem.actions.FindAction("Point");
    }

    void Update()
    {
        
    }

    void OnClick()
    {
        if(!clickAction.IsPressed())
        {
            return;
        }
        Vector2 screenPosition = pointAction.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"마우스 좌표 : {hit.point}" );
            GameManager.gameManager.ClickMouse(hit.point);
        }
        
    }

}

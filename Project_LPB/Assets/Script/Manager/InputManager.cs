using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Variables
    private InputAction clickAction;
    private InputAction pointAction;

    #endregion
    
#region Properties
    public Vector2 MousePos_screen {get; set;}
    public Vector3 MousePos_world {get; set;}

#endregion

#region Unity LifeCycle
    void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
        pointAction = InputSystem.actions.FindAction("Point");
    }

    void Update()
    {
        MousePos_screen = pointAction.ReadValue<Vector2>();
        Vector3 screenPos = new(MousePos_screen.x, MousePos_screen.y, -Camera.main.transform.position.z);
        MousePos_world = Camera.main.ScreenToWorldPoint(screenPos);
        MousePos_world = new Vector3(MousePos_world.x, MousePos_world.y, 0f);
    }

#endregion

#region Public Methods

#endregion

#region Private Methods
    private void OnClick()
    {
        if(!clickAction.IsPressed())
        {
            return;
        }
        Debug.Log($"마우스 좌표 : {MousePos_world}" );
        GameManager.Instance.ClickMouse(MousePos_world);
    }

#endregion



}

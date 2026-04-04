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
        Ray ray = Camera.main.ScreenPointToRay(MousePos_screen);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            MousePos_world = hit.point;
        }
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

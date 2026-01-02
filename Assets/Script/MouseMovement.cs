using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : MonoBehaviour
{
    public float mouseDistanceToMove = 1.0f;
    public float maxNormalSpeed = 5.0f;
    public float timeToMaxNormalSpeed = 1.0f;
    public float maxSpeedingSpeed = 8.0f;
    public float timeToMaxSpeedingSpeed = 1.5f;
    [SerializeField]
    private float accelerationTime = 0.0f;
    [SerializeField]
    private LayerMask groundLayer;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        Move();    
    }

    private void Move()
    {
        //마우스 좌표를 가져온 후 y좌표만 일치시켜서 2차원에서 거리를 측정하게 만든다.
        Vector3 mousePosition = GetMousePosition();
        mousePosition.y = transform.position.y;
        
        //마우스와 캐릭터 사이의 거리가 일정 이상일 경우만 움직임을 계속한다.
        float distanceToMouse = Vector3.Distance(mousePosition,transform.position);
        if(distanceToMouse < mouseDistanceToMove)
        {
            accelerationTime = 0.0f;
            return;
        }
        //캐릭터가 움직이는 방향 계산
        Vector3 movementDirection = (mousePosition - transform.position).normalized;
        
        //캐릭터가 움직이는 속도 계산
        accelerationTime += Time.deltaTime;
        float movementSpeed = maxNormalSpeed * Mathf.Min(1.0f, Mathf.Pow((accelerationTime * 1 / timeToMaxNormalSpeed), 3.0f));
        float timeFromNormalToSpeeding = timeToMaxSpeedingSpeed - timeToMaxNormalSpeed;
        movementSpeed += (maxSpeedingSpeed - maxNormalSpeed) * Mathf.Clamp((accelerationTime - timeToMaxNormalSpeed) / timeFromNormalToSpeeding, 0.0f, 1.0f);


        Vector3 movementVector = movementDirection * movementSpeed * Time.deltaTime; 

        controller.Move(movementVector);

        Debug.Log("movementSpeed : " + movementSpeed);
    }

    private Vector3 GetMousePosition()
    {
        Vector2 mousePosition_screen = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition_screen);
        Vector3 mousePosition_world = Vector3.zero;
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayer))
        {
            mousePosition_world = hit.point;
        }
        else
        {
            mousePosition_world = transform.position;
        }
        //Debug.Log("마우스 월드 좌표: " + mousePosition_world);

        return mousePosition_world;
    }
}

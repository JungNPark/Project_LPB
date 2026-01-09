using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MouseMovement : MonoBehaviour
{
    [Header("기타 설정")]
    [Tooltip("움직임을 시작하는 마우스와 캐릭터 사이의 거리")]
    public float mouseDistanceToMove = 1.0f;
    [Header("속도")]
    [Tooltip("일반 상태의 최대 속도")]
    public float maxNormalSpeed = 5.0f;
    [Tooltip("움직임을 시작한 후 일반 상태 최대 속도에 도달하기까지 걸리는 시간")]
    public float timeToMaxNormalSpeed = 1.0f;
    [Tooltip("과속 상태의 최대 속도")]
    public float maxSpeedingSpeed = 8.0f;
    [Tooltip("움직임을 시작한 후 과속 상태 최대 속도에 도달하기까지 걸리는 시간, timeToMaxNormalSpeed보다 커야 한다.")]
    public float timeToMaxSpeedingSpeed = 1.5f;
    [Header("과속 상태시 관성 정지 설정")]
    [Tooltip("과속 상태에서 관성 정지를 사용할 것인지 여부")]
    public bool bUseInertiaStopping = true;
    [Range(0.1f, 100.0f)]
    [Tooltip("관성 정지를 시작한 후 속도가 0이 될때까지 걸리는 시간")]
    public float decelerationTimeAtInertia = 1.5f;
    [Header("과속 상태 시 회전 제한 설정")]
    [Tooltip("과속 상태에서 회전 제한을 사용할 것인지 여부")]
    public bool bUseSpeedingRotation = true;
    [Tooltip("초당 회전 각도 제한")]
    public float speedingRotationLimit = 30.0f;
    [Header("--------------------------")]
    [Header("수치 확인용도")]
    [SerializeField]
    private float accelerationTime = 0.0f;
    [SerializeField]
    private Vector3 movementDirection;
    [SerializeField]
    private float movementSpeed = 0.0f;
    [SerializeField]
    private bool bIsSpeeding = false;
    [SerializeField]
    private bool bIsStopping = false;
    [SerializeField]
    private LayerMask groundLayer;
    private CharacterController controller;
    private Renderer objectRenderer;
    private Color originalColor;
    private Vector3 lastPosition;
    private LineRenderer dirRenderer;
    private TrailRenderer pathRenderer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    private void Start()
    {
        lastPosition = transform.position;

        // 방향 표시용 LineRenderer 설정
        dirRenderer = gameObject.AddComponent<LineRenderer>();
        dirRenderer.startWidth = 0.1f;
        dirRenderer.endWidth = 0.1f;
        dirRenderer.positionCount = 2;
        dirRenderer.material = new Material(Shader.Find("Sprites/Default"));
        dirRenderer.startColor = Color.red;
        dirRenderer.endColor = Color.red;

        // 이동 경로 표시용 TrailRenderer 설정
        pathRenderer = gameObject.AddComponent<TrailRenderer>();
        pathRenderer.startWidth = 0.1f;
        pathRenderer.endWidth = 0.1f;
        pathRenderer.time = 10.0f;
        pathRenderer.material = new Material(Shader.Find("Sprites/Default"));
        pathRenderer.startColor = Color.green;
        pathRenderer.endColor = Color.green;
    }
    private void Update()
    {
        //움직임
        Move(); 
        //디버깅용도   
        if (objectRenderer != null)
        {
            if (bIsSpeeding)
            {
                objectRenderer.material.color = Color.red;
            }
            else
            {
                objectRenderer.material.color = originalColor;
            }
        }
        
        // 방향 표시 업데이트
        dirRenderer.SetPosition(0, transform.position);
        dirRenderer.SetPosition(1, transform.position + movementDirection * 10.0f);

        lastPosition = transform.position;
    }

    private void Move()
    {
        //마우스 좌표를 가져온 후 y좌표만 일치시켜서 2차원에서 거리를 측정하게 만든다.
        Vector3 mousePosition = GetMousePosition();
        mousePosition.y = transform.position.y;
        
        //마우스와 캐릭터 사이의 거리가 일정 이하인 경우 정지 상태로 판단
        float distanceToMouse = Vector3.Distance(mousePosition,transform.position);
        if(distanceToMouse < mouseDistanceToMove)
        {
            bIsStopping = true;
        }
        else if(distanceToMouse >= mouseDistanceToMove && !bIsSpeeding)
        {
            bIsStopping = false;
        }

        //정지상태인 경우 정지 로직을 실행한다.
        if(bIsStopping)
        {
            accelerationTime = 0.0f;
            //과속상태일 경우 관성을 적용하면서 정지한다.
            if(bIsSpeeding && bUseInertiaStopping)
            {
                movementSpeed -= maxSpeedingSpeed * (1/decelerationTimeAtInertia) * Time.deltaTime;
                if(movementSpeed < 0.0f)
                {
                    movementSpeed = 0.0f;
                    bIsSpeeding = false;
                }
                controller.Move(movementDirection * movementSpeed * Time.deltaTime); 
            }
            return;
        }
        

        Vector3 mouseDirection = (mousePosition - transform.position).normalized;
        //관성상태일 때 움직임
        if(bIsSpeeding && bUseSpeedingRotation)
        {
            float rotationLimit = speedingRotationLimit * Mathf.Deg2Rad * Time.deltaTime;
            movementDirection = Vector3.RotateTowards(movementDirection, mouseDirection, rotationLimit, 0.0f);

            // float t = Vector3.Dot(movementDirection, mouseDirection);
            // t = Mathf.InverseLerp(1f, -1f, t);
            // float rotationLimit = Mathf.Lerp(180, 720, t) * Mathf.Deg2Rad * Time.deltaTime;

            // movementDirection = Vector3.RotateTowards(movementDirection, mouseDirection, rotationLimit, 0.0f);
            movementSpeed = Mathf.Lerp(2.0f, maxSpeedingSpeed, Mathf.Clamp01(Vector3.Dot(movementDirection, mouseDirection)));

        }
        else
        {
            //캐릭터가 움직이는 방향 계산
            movementDirection = mouseDirection;
            
            //캐릭터가 움직이는 속도 계산
            accelerationTime += Time.deltaTime;
            movementSpeed = maxNormalSpeed * Mathf.Min(1.0f, Mathf.Pow((accelerationTime * 1 / timeToMaxNormalSpeed), 3.0f));
            float timeFromNormalToSpeeding = timeToMaxSpeedingSpeed - timeToMaxNormalSpeed;
            movementSpeed += (maxSpeedingSpeed - maxNormalSpeed) * Mathf.Clamp((accelerationTime - timeToMaxNormalSpeed) / timeFromNormalToSpeeding, 0.0f, 1.0f);            
        }

        //캐릭터 움직임
        Vector3 movementVector = movementDirection * movementSpeed * Time.deltaTime; 
        controller.Move(movementVector);

        //과속 상태가 되었는지 확인
        if(accelerationTime > timeToMaxSpeedingSpeed)
        {
            bIsSpeeding = true;
        }

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

        return mousePosition_world;
    }
}

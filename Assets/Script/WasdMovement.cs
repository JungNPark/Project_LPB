using UnityEngine;
using UnityEngine.InputSystem;

public enum KeyUpDecelerationType
{
    Instant,
    Linear,
    EaseOut
}

public class WasdMovement : MonoBehaviour
{
    [Header("속도")]
    [Tooltip("최대 속도")]
    public float maxSpeed = 5.0f;
    [Tooltip("최소 속도가 최대 속도의 몇 배인지 설정")]
    [Range(0.0f, 1.0f)]
    public float minimumSpeedMultipler = 0.0f;
    [Header("움직임 설정")]
    [Tooltip("키보드에서 손을 뗀 경우 어떻게 감속하는지 설정")]
    [SerializeField]
    private KeyUpDecelerationType keyUpDecelerationType = KeyUpDecelerationType.Instant;
    [SerializeField]
    [Tooltip("반대 방향키 키보드를 누를 때 감속을 키울 것인지 여부")]
    private bool bUseCounterDeceleration = false;
    [Tooltip("반대 방향키를 눌렀을 때 감속 배수(기준 : 입력이 없을 때 감속)")]
    [SerializeField]
    private float counterDecelerationMultiplier = 2.0f;
    

    private Vector2 movementInput = Vector2.zero;
    private Vector2 inputDuration = Vector2.zero;
    private CharacterController controller;
    private Vector3 movementVector;
    private LineRenderer xRenderer;
    private LineRenderer zRenderer;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        xRenderer = CreateLineRenderer(Color.red, "XAxisRenderer");
        zRenderer = CreateLineRenderer(Color.blue, "ZAxisRenderer");

    }

    private void Update()
    {
        Move();

        if (xRenderer != null)
        {
            xRenderer.SetPosition(0, transform.position);
            xRenderer.SetPosition(1, transform.position + new Vector3(movementVector.x * 0.5f, 0, 0));
        }
        if (zRenderer != null)
        {
            zRenderer.SetPosition(0, transform.position);
            zRenderer.SetPosition(1, transform.position + new Vector3(0, 0, movementVector.z * 0.5f));
        }
    }

    public void OnInputMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
        Debug.Log($"InputMove ({movementInput.x}, {movementInput.y})");
    }

    private void Move()
    {
        //각 축 방향의 입력 시간을 계산
        inputDuration.x = UpdateInputDuration(movementInput.x, inputDuration.x);
        inputDuration.y = UpdateInputDuration(movementInput.y, inputDuration.y);

        //실제로 움직이는 벡터
        float movementSpeedX = 0f, movementSpeedZ = 0f;
        
        //각 벡터별 속도의 기본 값 적용
        movementSpeedX = Mathf.Pow(inputDuration.x, 3f) * maxSpeed;
        movementSpeedZ = Mathf.Pow(inputDuration.y, 3f) * maxSpeed;

        //키보드에서 손을 뗀 경우 KeyUpDecelerationType에 따라 감속 방식을 다르게 한 채로 movementSpeed 계산
        switch(keyUpDecelerationType)
        {
            case KeyUpDecelerationType.Instant:
                inputDuration.x = (movementInput.x == 0) ? 0 : inputDuration.x;
                inputDuration.y = (movementInput.y == 0) ? 0 : inputDuration.y;
                break;
            case KeyUpDecelerationType.Linear:
                movementSpeedX = (movementInput.x == 0) ? inputDuration.x * maxSpeed : movementSpeedX;      
                movementSpeedZ = (movementInput.y == 0) ? inputDuration.y * maxSpeed : movementSpeedZ;
                break;
            case KeyUpDecelerationType.EaseOut:
                //EaseOut인 경우 기존 설정처럼 진행
                break;
        }

        //입력 방향과 현재 movement방향이 일치할 경우 최소 속도 배수 적용
        movementSpeedX = (movementInput.x * inputDuration.x <= 0) ? (movementSpeedX) : (minimumSpeedMultipler * maxSpeed * movementInput.x + (1 - minimumSpeedMultipler) * movementSpeedX);
        movementSpeedZ = (movementInput.y * inputDuration.y <= 0) ? (movementSpeedZ) : (minimumSpeedMultipler * maxSpeed * movementInput.y + (1 - minimumSpeedMultipler) * movementSpeedZ);

        //최종적으로 이동
        movementVector = new Vector3(movementSpeedX, 0, movementSpeedZ);

        //움직임
        controller.Move(movementVector * Time.deltaTime);
    }

    private float UpdateInputDuration(in float movementInput, in float preInputDuration)
    {
        float returnInputDuration = preInputDuration;
        //키보드에서 손을 뗀 경우
        if (movementInput == 0)
        {
            float temp = preInputDuration + Time.deltaTime * ((preInputDuration < 0) ? 1 : -1);
            //temp와 preInputDuration이 음수인 경우 즉, 기존 방향과 반대방향(곱하면 음수)까지 속도가 감소된 경우는 0으로 변경한다.
            //예를 들어 +방향으로 향하다가 방향키에서 KeyUp을 했는데 속도가 너무 감소해서 -방향으로 순간 움직이는 경우 움직임이 어색할 것이다.
            returnInputDuration = ((temp * preInputDuration <= 0) ? 0 : temp);
        }
        else
        {      
            if(bUseCounterDeceleration && preInputDuration * movementInput < 0)
            {
                float temp = preInputDuration + Time.deltaTime * (counterDecelerationMultiplier-1) * movementInput;
                returnInputDuration = ((temp * preInputDuration <= 0) ? 0 : temp);
            }      
            returnInputDuration += Time.deltaTime * movementInput;
            //bUseCounterDeceleration 값에 따라 반대방향키를 누르면 추가 감속을 적용한다.
            returnInputDuration = Mathf.Clamp(returnInputDuration, -1f, 1f);
        }

        return returnInputDuration;
    }

    private LineRenderer CreateLineRenderer(Color color, string name)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;

        LineRenderer lr = go.AddComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.positionCount = 2;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = color;
        lr.endColor = color;
        return lr;
    }
}

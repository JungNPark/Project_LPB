using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
#region Variables
    private LineRenderer lineRenderer;
    [SerializeField]
    private float lineLength = 5.0f;
    private bool bCanShoot = false; // 평소 발사 여부를 제어하는 변수 추가
    public GameObject ballStartPoint; // 턴 시작 시 공들이 모일 오브젝트

#endregion

#region Properties
    public BallBase[] balls { get; set;}
    public Enemy[] enemys { get; set; }
    public InputManager InputManager { get; set; }
    public static GameManager Instance { get; set; }

    public event Action OnTurnStart;

#endregion

#region Unity LifeCycle
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        InputManager = FindObjectsByType<InputManager>(FindObjectsSortMode.None)[0];
        balls = FindObjectsByType<BallBase>(FindObjectsSortMode.None);
        enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
    }

    void Start()
    {
        InitLineRenderer();
        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        DrawShootingLine();
    }

#endregion

#region Public Methods
    public void ClickMouse(Vector3 mousePosInWorld)
    {
        Vector3 ballPos = balls[0].transform.position;
        Vector3 shootingDir = (mousePosInWorld - ballPos).normalized;

        Debug.Log($"공 발사 방향 벡터 : {shootingDir}");
        ShootBalls(shootingDir);
    }

    public void ShootBalls(Vector3 dir)
    {
        if (!bCanShoot) return; // 안전을 위해 한 번 더 검증
        
        bCanShoot = false; // 중복해서 여러 번 쏘지 못하도록 처리

        lineRenderer.enabled = false;
        foreach(IBall ball in balls)
        {
            ball.Shoot(dir);
        }
    }

#endregion

#region Private Methods
    private void InitLineRenderer()
    {
        // 라인을 그리기 위한 LineRenderer 컴포넌트 추가 및 초기화
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            // 화면에 잘 보이도록 기본 머티리얼 및 색상 설정
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.lightCyan;
        }
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }


    private void DrawShootingLine()
    {
        if(!bCanShoot)
        {
            return;
        }
        Vector3 mousePos = new Vector3(InputManager.MousePos_world.x, balls[0].transform.position.y, InputManager.MousePos_world.z);
        Vector3 lineStart = balls[0].transform.position;
        Vector3 lineDir = (mousePos - lineStart).normalized;
        Vector3 lineEnd = lineStart + lineDir * lineLength;
        lineRenderer.SetPosition(0, lineStart);
        lineRenderer.SetPosition(1, lineEnd);
    }

    private void StartTurn()
    {
        // 모든 공을 지정된 오브젝트의 위치로 이동
        if (ballStartPoint != null && balls != null)
        {
            foreach (BallBase ball in balls)
            {
                ball.transform.position = ballStartPoint.transform.position;
            }
        }
        lineRenderer.enabled = true; // 궤적 라인 다시 켜기
        bCanShoot = true; // 턴이 시작되면 다시 쏠 수 있도록 상태 변경

        OnTurnStart?.Invoke();
    }

#endregion





    
}

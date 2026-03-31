using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BallBase[] balls { get; set;}
    public Enemy[] enemys { get; set; }
    public float lineLength = 5.0f;
    public bool bIsBallShooted = false;
    public static GameManager gameManager;
    public InputManager inputManager;
    private LineRenderer lineRenderer;

    void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        inputManager = FindObjectsByType<InputManager>(FindObjectsSortMode.None)[0];
        balls = FindObjectsByType<BallBase>(FindObjectsSortMode.None);
        enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
    }

    void Start()
    {
        InitLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        DrawShootingLine();
    }

    public void ClickMouse(Vector3 mousePosInWorld)
    {
        Vector3 ballPos = balls[0].transform.position;
        Vector3 shootingDir = (mousePosInWorld - ballPos).normalized;

        Debug.Log($"공 발사 방향 벡터 : {shootingDir}");
        ShootBalls(shootingDir);
    }

    public void ShootBalls(Vector3 dir)
    {

        bIsBallShooted = true;

        lineRenderer.enabled = false;
        foreach(IBall ball in balls)
        {
            ball.Shoot(dir);
        }
    }

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
        if(bIsBallShooted)
        {
            return;
        }
        Vector3 mousePos = new Vector3(inputManager.mousePos_world.x, balls[0].transform.position.y, inputManager.mousePos_world.z);
        Vector3 lineStart = balls[0].transform.position;
        Vector3 lineDir = (mousePos - lineStart).normalized;
        Vector3 lineEnd = lineStart + lineDir * lineLength;
        lineRenderer.SetPosition(0, lineStart);
        lineRenderer.SetPosition(1, lineEnd);
    }


    
}


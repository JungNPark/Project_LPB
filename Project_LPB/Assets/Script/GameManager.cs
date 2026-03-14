using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BallCtrl[] balls { get; set;}
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
        balls = FindObjectsByType<BallCtrl>(FindObjectsSortMode.None);
        enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
    }
    void Start()
    {
        InitLineRenderer();
    }
    // Update is called once per frame
    void Update()
    {
        // 공과 입력 매니저가 유효할 때, 공에서 마우스 위치까지 라인을 업데이트
        DrawShootingLine();
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

    public void ClickMouse(Vector3 mousePosInWorld)
    {
        Vector3 ballPos = balls[0].transform.position;
        Vector2 shootingDir = new Vector2(mousePosInWorld.x - ballPos.x, mousePosInWorld.z - ballPos.z).normalized;

        Debug.Log($"공 발사 방향 벡터 : {shootingDir}");
        ShootBalls(shootingDir);
    }

    public void ShootBalls(Vector2 dir)
    {

        bIsBallShooted = true;
        //그려진 lineRenderer 지우기
        lineRenderer.enabled = false;
        foreach(BallCtrl ball in balls)
        {
            ball.Shoot(dir);
        }
    }
}

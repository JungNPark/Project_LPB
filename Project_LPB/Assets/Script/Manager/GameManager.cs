using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
<<<<<<< Updated upstream:Project_LPB/Assets/Script/GameManager.cs
    public BallCtrl[] balls;
=======
    public BallBase[] balls { get; set;}
    
    public Enemy[] enemys { get; set; }
    public float lineLength = 5.0f;
    public bool bIsBallShooted = false;
>>>>>>> Stashed changes:Project_LPB/Assets/Script/Manager/GameManager.cs
    public static GameManager gameManager;

    void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
<<<<<<< Updated upstream:Project_LPB/Assets/Script/GameManager.cs
=======
        inputManager = FindObjectsByType<InputManager>(FindObjectsSortMode.None)[0];
        balls = FindObjectsByType<BallBase>(FindObjectsSortMode.None);
        enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
>>>>>>> Stashed changes:Project_LPB/Assets/Script/Manager/GameManager.cs
    }
    void Start()
    {
        balls = FindObjectsByType<BallCtrl>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickMouse(Vector3 mousePosInWorld)
    {
        Vector3 ballPos = balls[0].transform.position;
<<<<<<< Updated upstream:Project_LPB/Assets/Script/GameManager.cs
        Vector2 shootingDir = new Vector2(mousePosInWorld.x - ballPos.x, mousePosInWorld.z - ballPos.z);
=======
        Vector3 shootingDir = (mousePosInWorld - ballPos).normalized;
>>>>>>> Stashed changes:Project_LPB/Assets/Script/Manager/GameManager.cs

        Debug.Log($"공 발사 방향 벡터 : {shootingDir}");
        ShootBalls(shootingDir);
    }

    public void ShootBalls(Vector3 dir)
    {
<<<<<<< Updated upstream:Project_LPB/Assets/Script/GameManager.cs
        foreach(BallCtrl ball in balls)
=======

        bIsBallShooted = true;
        //그려진 lineRenderer 지우기
        lineRenderer.enabled = false;
        foreach(IBall ball in balls)
>>>>>>> Stashed changes:Project_LPB/Assets/Script/Manager/GameManager.cs
        {
            ball.Shoot(dir);
        }
    }
}

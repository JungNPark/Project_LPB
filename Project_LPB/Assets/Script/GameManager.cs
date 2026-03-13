using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BallCtrl[] balls;
    public static GameManager gameManager;

    void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
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
        Vector2 shootingDir = new Vector2(mousePosInWorld.x - ballPos.x, mousePosInWorld.z - ballPos.z);

        Debug.Log($"공 발사 방향 벡터 : {shootingDir}");
        ShootBalls(shootingDir);
    }

    public void ShootBalls(Vector2 dir)
    {
        foreach(BallCtrl ball in balls)
        {
            ball.Shoot(dir);
        }
    }
}

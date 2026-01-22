using UnityEngine;
using UnityEngine.Analytics;

public class ArrivalGoalObject : MonoBehaviour
{
    public float requiredStayTime = 2.0f;
    private float goalStayTime = 0.0f;
    private bool bIsEnter = false;
    private bool bIsGoal = false;

    private LineRenderer progressRenderer;
    public float radius = 1.0f;
    public int segments = 50;
    public Color progressColor = Color.green;
    public float heightOffset = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupProgressRenderer();
    }

    void SetupProgressRenderer()
    {
        GameObject go = new GameObject("ProgressRenderer");
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.up * heightOffset;
        go.transform.localRotation = Quaternion.Euler(90, 0, 0);

        progressRenderer = go.AddComponent<LineRenderer>();
        progressRenderer.useWorldSpace = false;
        progressRenderer.startWidth = 0.1f;
        progressRenderer.endWidth = 0.1f;
        progressRenderer.material = new Material(Shader.Find("Sprites/Default"));
        progressRenderer.startColor = progressColor;
        progressRenderer.endColor = progressColor;
        progressRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(bIsGoal)
        {
            return;
        }
        //트리거 시간 업데이트
        if(bIsEnter)
        {
            goalStayTime = Mathf.Min(requiredStayTime, goalStayTime + Time.deltaTime);
        }
        else
        {
            goalStayTime = Mathf.Max(0.0f, goalStayTime - Time.deltaTime);
        }

        UpdateProgressBar();

        if(goalStayTime >= requiredStayTime)
        {
            Goal();
        }
    }

    void UpdateProgressBar()
    {
        if (progressRenderer == null) return;

        float ratio = Mathf.Clamp01(goalStayTime / requiredStayTime);

        if (ratio <= 0.0f)
        {
            progressRenderer.positionCount = 0;
            return;
        }

        float angle = 360f * ratio;
        int pointCount = (int)(segments * ratio) + 2;
        progressRenderer.positionCount = pointCount;

        for (int i = 0; i < pointCount; i++)
        {
            float currentAngleDeg = (float)i / (pointCount - 1) * angle;
            float rad = currentAngleDeg * Mathf.Deg2Rad;

            float x = Mathf.Sin(rad) * radius;
            float y = Mathf.Cos(rad) * radius;

            progressRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    void Goal()
    {
        //StageManager 호출
        bIsGoal = true;
        StageManager.instance.UpdateGoalScore(1);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Goal")
        {
            return;
        }

        bIsEnter = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag != "Goal")
        {
            return;
        }

        bIsEnter = false;
        bIsGoal = false;
    }
}

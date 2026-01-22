using UnityEngine;

public class StageManager : MonoBehaviour
{

    public int TargetScore = 1;
    private int currentScore = 0;
    public GameObject stageClearUI;
    public static StageManager instance { get; private set;}

    void Awake()
    {
        instance = this;
    }

    public void UpdateGoalScore(int change)
    {
        currentScore += change;
        if(currentScore >= TargetScore)
        {
            StageClear();
        }
    }

    void StageClear()
    {
        Debug.Log("Stage Clear!");
        //이 주석 아래에 Stage Clear UI를 띄우는 부분을 추가해줘
        if (stageClearUI != null)
        {
            stageClearUI.SetActive(true);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

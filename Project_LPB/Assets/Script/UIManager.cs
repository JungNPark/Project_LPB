using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    private Dictionary<Enemy, TextMeshProUGUI> hpTextDict = new Dictionary<Enemy, TextMeshProUGUI>();
    private Canvas mainCanvas;
    private TextMeshProUGUI ballStatText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 1. 씬 내에 Canvas가 있는지 확인하고, 없다면 생성합니다.
        Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        mainCanvas = canvases[0];

        InitEnemyHpUI();
        // Ball Stat이라는 이름의 오브젝트를 찾아서 TextMeshProUGUI 컴포넌트를 가져옵니다.
        InitBallStatUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyHpUI();
        UpdateBallStatUI();
    }
    private void InitBallStatUI()
    {
        GameObject ballStatObj = GameObject.Find("Ball Stat");
        if (ballStatObj != null)
        {
            ballStatText = ballStatObj.GetComponent<TextMeshProUGUI>();
        }
    }

    private void InitEnemyHpUI()
    {
        // 2. 적 리스트를 바탕으로 HP를 표시할 텍스트 UI를 생성합니다.
        if (GameManager.gameManager != null && GameManager.gameManager.enemys != null)
        {
            foreach (Enemy enemy in GameManager.gameManager.enemys)
            {
                GameObject textObj = new GameObject($"{enemy.name}_HP_Text");
                textObj.transform.SetParent(mainCanvas.transform, false);

                TextMeshProUGUI hpText = textObj.AddComponent<TextMeshProUGUI>();
                hpText.fontSize = 32;
                hpText.alignment = TextAlignmentOptions.Center;
                hpText.color = Color.white;

                hpTextDict.Add(enemy, hpText);
            }
        }
        else
        {
            Debug.Log("Can't Find enemys");
        }
    }


    private void UpdateEnemyHpUI()
    {
        foreach (Enemy enemy in GameManager.gameManager.enemys)
        {
            if (hpTextDict.TryGetValue(enemy, out TextMeshProUGUI hpText))
            {
                // 적이 살아있을 때(활성화 상태)만 UI를 갱신합니다.
                if (enemy.gameObject.activeSelf)
                {
                    hpText.gameObject.SetActive(true);
                    hpText.text = $"{Mathf.CeilToInt(enemy.stat.nowHp)}";

                    // 월드 좌표를 화면(Screen) 좌표로 변환하여 적 머리 위(Y축 +1.5f)에 띄웁니다.
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
                    hpText.transform.position = screenPos;

                    // 카메라 뒤로 넘어갔을 경우 텍스트를 숨깁니다.
                    if (screenPos.z < 0)
                    {
                        hpText.gameObject.SetActive(false);
                    }
                }
                else
                {
                    // 적이 비활성화(처치됨) 상태라면 텍스트도 숨깁니다.
                    hpText.gameObject.SetActive(false);
                }
            }
        }
    }

    private void UpdateBallStatUI()
    {
        // 공의 상태 정보를 UI 텍스트에 출력합니다.
        if (ballStatText != null && GameManager.gameManager != null && GameManager.gameManager.balls != null && GameManager.gameManager.balls.Length > 0)
        {
            ballStatText.text = $"[Ball Stat]\n" + 
                                $"speed : {GameManager.gameManager.balls[0].stat.speed}\n" +
                                $"Dir : {GameManager.gameManager.balls[0].stat.dir}\n" + 
                                $"Damage : {GameManager.gameManager.balls[0].stat.damage}\n" + 
                                $"Size : {GameManager.gameManager.balls[0].stat.size}\n";
        }
    }
}

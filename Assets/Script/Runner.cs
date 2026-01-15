using UnityEngine;
using System.Collections;
using UnityEngine.Scripting.APIUpdating;
using Unity.VisualScripting;


[RequireComponent(typeof(CharacterController))]
public class Runner : MonoBehaviour
{

    public float detectInterval = 0.1f;
    public float detectRange_outer = 5.0f;
    public float detectRange_inner = 2.5f;
    public float fleeDration = 1.0f;
    public float movementSpeed = 10.0f;
    public float panicTime = 1.0f;
    public float panicSpeedMultiplier = 1.5f;

    public float boxCastSize = 0.5f;

    public LayerMask groundLayer;
    public LayerMask chaserLayer;

    private Vector3 movementDirection = Vector3.zero;
    private bool bIsFleeing = false;
    private bool bIsPanic = false;
    private CharacterController controller;
    private float detectFailedTime = 0.0f;
    private float panicDuration = 0.0f;
    private Renderer objectRenderer;
    private Color originalColor;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    void Start()
    {
        //주변 Chaser를 감지한 후 도망가는 코루틴 실행
        StartCoroutine(DetermineFleeingStrategy());
    }

    void Update()
    {
        if(bIsFleeing)
        {
            Move();
        }

        if (objectRenderer != null)
        {
            objectRenderer.material.color = bIsPanic ? Color.red : originalColor;
        }
    }

    void Move()
    {
        if(bIsPanic)
        {
            controller.Move(movementDirection * movementSpeed * panicSpeedMultiplier* Time.deltaTime);
            return;   
        }
        controller.Move(movementDirection * movementSpeed * Time.deltaTime);
    }

    IEnumerator DetermineFleeingStrategy()
    {
        WaitForSeconds waitflag = new WaitForSeconds(detectInterval);
        //패닉 시간 업데이트 및 패닉 상태 검사
        while(true)
        {
            panicDuration += detectInterval;
            if(panicDuration >= panicTime)
            {
                bIsPanic = false;
                panicDuration = 0.0f;
            }
            if(bIsPanic)
            {
                yield return waitflag;
                continue;
            }
            Vector3 runDirSum = Vector3.zero;
            Collider[] chasers = Physics.OverlapSphere(transform.position, detectRange_outer);

            detectFailedTime += detectInterval;
            foreach(Collider chaser in chasers)
            {
                if(chaser.gameObject.tag != "Chaser")
                {
                    continue;
                }
                bIsFleeing = true;
                detectFailedTime = 0.0f;
                Vector3 runDir = (transform.position - chaser.transform.position);
                //y축은 무시하도록 변경
                runDir.y = 0;
                runDir = runDir.normalized;
                runDirSum += runDir;
            }
            if(runDirSum != Vector3.zero)
            {
                movementDirection = runDirSum.normalized;
            }
            if(bIsFleeing && detectFailedTime >= fleeDration)
            {
                bIsFleeing = false;
            }

            //진행 방향에 벽이 있는지 확인
            Vector3 boxCastBaseScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.1f, transform.localScale.z);
            Vector3 halfExtents = boxCastBaseScale * boxCastSize;
            RaycastHit hit;
            bool isHit = Physics.BoxCast(transform.position, halfExtents, movementDirection, out hit, transform.rotation, detectRange_inner, groundLayer);

            Color color = isHit ? Color.red : Color.green;
            float distance = isHit ? hit.distance : detectRange_outer;
            Debug.DrawRay(transform.position, movementDirection * distance, color);
            DrawBox(transform.position + movementDirection * distance, halfExtents, transform.rotation, color);

            if(isHit)
            {
                Debug.Log("Detect Wall");
                bIsFleeing = false;
                //벽이 있는 경우, detectRange_inner 범위에도 Chaser가 있는지 확인
                chasers = Physics.OverlapSphere(transform.position, detectRange_inner);
                foreach(Collider chaser in chasers)
                {
                    if(chaser.gameObject.tag != "Chaser")
                    {
                        continue;
                    }
                    //플레이어와 벽이 없는 무작위 방향 추출
                    do
                    {
                        movementDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
                        movementDirection = movementDirection.normalized;
                    
                    }while(Physics.BoxCast(transform.position, halfExtents, movementDirection, out hit, transform.rotation, detectRange_outer, groundLayer) || Physics.BoxCast(transform.position, halfExtents, movementDirection, out hit, transform.rotation, detectRange_outer, chaserLayer));
                    //panic 상태에 대한 설정 추가
                    Debug.Log("Panic");
                    bIsPanic = true;
                    panicDuration = 0.0f;
                    bIsFleeing = true;
                    break;
                }
            }
                

            yield return waitflag;
        }
    }

    private void DrawBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color)
    {
        Vector3[] points = new Vector3[8];
        Vector3 x = orientation * new Vector3(halfExtents.x, 0, 0);
        Vector3 y = orientation * new Vector3(0, halfExtents.y, 0);
        Vector3 z = orientation * new Vector3(0, 0, halfExtents.z);

        points[0] = center + x + y + z;
        points[1] = center - x + y + z;
        points[2] = center - x - y + z;
        points[3] = center + x - y + z;
        points[4] = center + x + y - z;
        points[5] = center - x + y - z;
        points[6] = center - x - y - z;
        points[7] = center + x - y - z;

        Debug.DrawLine(points[0], points[1], color);
        Debug.DrawLine(points[1], points[2], color);
        Debug.DrawLine(points[2], points[3], color);
        Debug.DrawLine(points[3], points[0], color);

        Debug.DrawLine(points[4], points[5], color);
        Debug.DrawLine(points[5], points[6], color);
        Debug.DrawLine(points[6], points[7], color);
        Debug.DrawLine(points[7], points[4], color);

        Debug.DrawLine(points[0], points[4], color);
        Debug.DrawLine(points[1], points[5], color);
        Debug.DrawLine(points[2], points[6], color);
        Debug.DrawLine(points[3], points[7], color);
    }
}

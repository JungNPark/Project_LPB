using UnityEngine;
using System.Collections;
using UnityEngine.Scripting.APIUpdating;


[RequireComponent(typeof(CharacterController))]
public class Runner : MonoBehaviour
{

    public float detectInterval = 0.1f;
    public float detectRange = 5.0f;
    public float fleeDration = 1.0f;
    public float movementSpeed = 10.0f;

    private Vector3 movementDirection = Vector3.zero;
    private bool bIsFleeing = false;
    private CharacterController controller;
    private float detectFailedTime = 0.0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        //주변 Chaser를 감지한 후 도망가는 코루틴 실행
        StartCoroutine(DetectChaser());
    }

    void Update()
    {
        if(bIsFleeing)
        {
            Move();
        }
    }

    void Move()
    {
        controller.Move(movementDirection * movementSpeed * Time.deltaTime);
    }

    IEnumerator DetectChaser()
    {
        WaitForSeconds waitflag = new WaitForSeconds(detectInterval);
        while(true)
        {
            Vector3 runDirSum = Vector3.zero;
            Collider[] chasers = Physics.OverlapSphere(transform.position, detectRange);

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
            if(bIsFleeing && detectFailedTime >= fleeDration)
            {
                bIsFleeing = false;
            }

            if(runDirSum != Vector3.zero)
            {
                movementDirection = runDirSum.normalized;
            }

            yield return waitflag;
        }
    }
}

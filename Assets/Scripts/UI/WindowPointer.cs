using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowPointer : MonoBehaviour
{
    Vector2 target;

    [SerializeField]
    RectTransform pointerRectTransform;

    EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(200, 45);
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        target = enemySpawner.getCurrentMoneyPos();

        Vector2 toPosition = target;
        Vector2 fromPosition = Camera.main.transform.position;

        Vector2 dir = (toPosition - fromPosition).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x);
        // Convert in degrees
        angle = angle * Mathf.Rad2Deg;

        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle - 90);

        Vector2 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(target);

        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width ||
                            targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;

        pointerRectTransform.gameObject.SetActive(false);
        if (isOffScreen)
        {
            pointerRectTransform.gameObject.SetActive(true);

            Vector2 capTargetScreenPos = targetPositionScreenPoint;

            float offset = 12f;

            capTargetScreenPos.x = Mathf.Clamp(capTargetScreenPos.x,offset , Screen.width-offset);
            capTargetScreenPos.y = Mathf.Clamp(capTargetScreenPos.y, offset, Screen.height-offset);

            //Vector2 pointerWorldPos = Camera.main.ScreenToWorldPoint(capTargetScreenPos);
            pointerRectTransform.position = capTargetScreenPos;

        }

    }
}

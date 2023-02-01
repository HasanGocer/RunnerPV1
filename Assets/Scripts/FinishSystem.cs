using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSystem : MonoSingleton<FinishSystem>
{
    [SerializeField] private GameObject cameraPos, cameraLookPos;
    [SerializeField] private int nowShakeCount, maxShakeCount;
    [SerializeField] private int finishRate;

    public void FinishTime()
    {
        CamMoveControl.Instance.enabled = false;
        Camera.main.gameObject.transform.position = cameraPos.transform.position;
        Camera.main.gameObject.transform.LookAt(cameraLookPos.transform);
    }
}

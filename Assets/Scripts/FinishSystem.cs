using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSystem : MonoSingleton<FinishSystem>
{
    [SerializeField] private GameObject cameraPos;
    [SerializeField] private int nowShakeCount, maxShakeCount;
    [SerializeField] private int finishRate;


}

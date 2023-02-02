using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[System.Serializable]
public class FinishSystem : MonoSingleton<FinishSystem>
{
    [Header("Camera_Ayar_Field")]

    [SerializeField] private GameObject cameraPos;
    [SerializeField] private GameObject cameraLookPos;

    [Space(10)]

    [Header("Shake_Field")]

    [SerializeField] private GameObject shakePanel;
    [SerializeField] private GameObject shakePotionGO;
    [SerializeField] private int nowShakeCount, maxShakeCount;
    [SerializeField] private float shakeTime, shakeStrength;
    [SerializeField] private Button shakeButton;
    [SerializeField] Vector3 tempScale;

    [Space(10)]

    [Header("Thros_Field")]

    [SerializeField] private float throwStrength;
    [SerializeField] private float throwTime, throwAfterTime;
    [SerializeField] private GameObject throwFinishPos;
    [SerializeField] private GameObject throwFinishPartical;

    [Space(10)]

    [Header("Thros_Field")]

    [SerializeField] private float growFactor;
    private GameObject growPotion;
    [SerializeField] private GameObject growObject;

    public void StartFinish()
    {
        shakeButton.onClick.AddListener(() => StartCoroutine(ShakeTime()));
    }

    public void FinishTime()
    {
        CamMoveControl.Instance.enabled = false;
        Camera.main.gameObject.transform.position = cameraPos.transform.position;
        Camera.main.gameObject.transform.LookAt(cameraLookPos.transform);
        tempScale = shakePotionGO.transform.localScale;
        ShakePanelTrue();
    }

    private void ShakePanelTrue()
    {
        shakePanel.SetActive(true);
        shakePotionGO.SetActive(true);
    }
    private IEnumerator ShakeTime()
    {
        nowShakeCount++;
        if (nowShakeCount == maxShakeCount) shakeButton.gameObject.SetActive(false);
        shakePotionGO.transform.DOShakeScale(shakeTime, shakeStrength);
        yield return new WaitForSeconds(shakeTime);
        shakePotionGO.transform.localScale = tempScale;
        if (nowShakeCount == maxShakeCount) StartCoroutine(FinalyFinishTime());
    }
    private IEnumerator FinalyFinishTime()
    {
        shakePanel.SetActive(false);
        shakePotionGO.transform.DOJump(throwFinishPos.transform.position, throwStrength, (int)throwStrength, throwTime);
        yield return new WaitForSeconds(throwTime);
        growPotion = Instantiate(throwFinishPartical, throwFinishPos.transform.position, throwFinishPos.transform.rotation);
        yield return new WaitForSeconds(throwAfterTime);
        StartCoroutine(FinishGrow());
    }
    private IEnumerator FinishGrow()
    {
        growObject.SetActive(true);
        float growTempFactor = (GameManager.Instance.addedMoney / 50);
        growPotion.transform.DOMove(new Vector3(growPotion.transform.position.x, growPotion.transform.position.y + growTempFactor, growPotion.transform.position.z), growTempFactor * growFactor).SetEase(Ease.InOutSine);
        growObject.transform.DOScale(new Vector3(1, growTempFactor * 2, 1), growTempFactor * growFactor);
        yield return new WaitForSeconds(growFactor * growTempFactor);
        Buttons.Instance.winPanel.SetActive(true);
        Buttons.Instance.finishGameMoneyText.text = (growTempFactor * GameManager.Instance.addedMoney).ToString();
    }
}

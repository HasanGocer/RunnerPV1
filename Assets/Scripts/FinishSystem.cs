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
    [SerializeField] private GameObject throwLoopPartical;

    [Space(10)]

    [Header("Thros_Field")]

    [SerializeField] private float growFactor;
    [SerializeField] private float growMaxCount;
    private GameObject growPotion;
    private GameObject growLoopPotion;
    bool isFinish;
    [SerializeField] private GameObject growObject;

    public void StartFinish()
    {
        shakeButton.onClick.AddListener(() => StartCoroutine(ShakeTime()));
    }

    public void FinishTime()
    {
        CamMoveControl.Instance.target = shakePotionGO;
        CamMoveControl.Instance.time = 1;
        tempScale = shakePotionGO.transform.localScale;
        ShakePanelTrue();
    }

    private IEnumerator CameraLerp()
    {
        while (true)
        {
            Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, cameraPos.transform.position, Time.deltaTime * 6);
            yield return new WaitForSeconds(Time.deltaTime);
            if (1 > Vector3.Distance(Camera.main.gameObject.transform.position, cameraPos.transform.position))
            {
                StartCoroutine(CameraLook());
                ShakePanelTrue();
                break;
            }
        }
    }
    private IEnumerator CameraLook()
    {
        Camera.main.gameObject.transform.DOLookAt(cameraLookPos.transform.position, 0.3f);
        yield return new WaitForSeconds(0.3f);
    }
    private void ShakePanelTrue()
    {
        shakePanel.SetActive(true);
        shakePotionGO.SetActive(true);
        CharacterManager.Instance.character.SetActive(true);
        for (int i = 0; i < 4; i++)
            if (CharacterManager.Instance.character.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                shakePotionGO.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        CharacterManager.Instance.character.SetActive(false);
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
        if (!isFinish)
        {
            isFinish = true;
            shakePanel.SetActive(false);
            shakePotionGO.transform.DOJump(throwFinishPos.transform.position, throwStrength, (int)throwStrength, throwTime);
            yield return new WaitForSeconds(throwTime);
            shakePotionGO.SetActive(false);
            growPotion = Instantiate(throwFinishPartical, throwFinishPos.transform.position, throwFinishPos.transform.rotation);
            growLoopPotion = Instantiate(throwLoopPartical, throwFinishPos.transform.position, throwFinishPos.transform.rotation);
            yield return new WaitForSeconds(throwAfterTime);
            SoundSystem.Instance.CallSise();
            growPotion.SetActive(false);
            StartCoroutine(FinishGrow());
        }
    }
    private IEnumerator FinishGrow()
    {
        growObject.SetActive(true);
        float growTempFactor = (GameManager.Instance.addedMoney / 20) * growFactor;
        if (growTempFactor > growMaxCount) growTempFactor = growMaxCount;
        CamMoveControl.Instance.target = growLoopPotion;
        growLoopPotion.transform.DOMove(new Vector3(growLoopPotion.transform.position.x, growLoopPotion.transform.position.y + growTempFactor, growLoopPotion.transform.position.z), growTempFactor / growFactor).SetEase(Ease.InOutSine);
        growObject.transform.DOScale(new Vector3(1, growTempFactor * 2, 1), growTempFactor / growFactor);
        yield return new WaitForSeconds(growFactor * growTempFactor / growFactor);
        StartCoroutine(ParticalSystem.Instance.CallFinishPartical(growLoopPotion));
        Buttons.Instance.winPanel.SetActive(true);
        Buttons.Instance.finishGameMoneyText.text = (GameManager.Instance.addedMoney).ToString();
    }
}

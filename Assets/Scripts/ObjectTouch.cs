using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectTouch : MonoBehaviour
{
    public enum PotionStat
    {
        level1 = 1,
        level2 = 2,
        level3 = 3,
        level4 = 4
    }

    public PotionStat enumStat = PotionStat.level1;
    public bool isActive;
    [SerializeField] private ObjectMovement objectMovement;
    bool isConvert = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion") && isActive)
            StartCoroutine(StackMechanic.Instance.AddNewObject(other.gameObject));
        if (other.CompareTag("PConverter") && enumStat == PotionStat.level1 && isActive)
        {
            PointText.Instance.CallPointMoneyText(CharacterManager.Instance.character, 1, true);
            SoundSystem.Instance.CallSise();
            StartCoroutine(ParticalSystem.Instance.CallPotionPartical(gameObject));
            StartCoroutine(ConvertObjectBool());
        }
        if (other.CompareTag("SConverter") && isActive && enumStat == PotionStat.level2 || enumStat == PotionStat.level3)
        {
            PointText.Instance.CallPointMoneyText(CharacterManager.Instance.character, 1, true);
            StartCoroutine(ParticalSystem.Instance.CallConvertPartical(gameObject));
            StartCoroutine(ConvertObjectBool());
        }
        if (other.CompareTag("DConverter") && enumStat > PotionStat.level1)
        {
            PointText.Instance.CallPointMoneyText(CharacterManager.Instance.character, 1, false);
            StartCoroutine(ConvertDownObjectBool());
        }
        if ((other.CompareTag("FinishConverter") || other.CompareTag("Finish")) && isActive)
            Sell(other.gameObject);
        if (other.CompareTag("Crusher") && isActive)
            StackMechanic.Instance.CrashObjects(gameObject, objectMovement);
    }
    private void Sell(GameObject other)
    {
        isActive = false;
        StartCoroutine(other.GetComponent<FinishConverterManager>().SellItem(gameObject, (int)enumStat, false));

    }
    private IEnumerator ConvertObjectBool()
    {
        if (!isConvert)
        {
            isConvert = true;
            enumStat++;
            StartCoroutine(StackMechanic.Instance.ObjectConverter(gameObject, (int)enumStat));
            yield return new WaitForSeconds(0.2f);
            isConvert = false;
        }
    }
    private IEnumerator ConvertDownObjectBool()
    {
        if (!isConvert)
        {
            isConvert = true;
            enumStat--;
            StackMechanic.Instance.ObjectConverterDown(gameObject, (int)enumStat);
            yield return new WaitForSeconds(0.2f);
            isConvert = false;
        }
    }
}

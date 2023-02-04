using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterTouch : MonoBehaviour
{
    public enum PotionStat
    {
        level1 = 1,
        level2 = 2,
        level3 = 3,
        level4 = 4
    }

    public PotionStat enumStat = PotionStat.level1;
    bool isConvert = false;

    public void StartButton()
    {
        StackMechanic.Instance.StackObjects.Add(gameObject);
        StackMechanic.Instance.objectMovements.Add(gameObject.GetComponent<ObjectMovement>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
            StartCoroutine(StackMechanic.Instance.AddNewObject(other.gameObject));
        if (other.CompareTag("PConverter") && enumStat == PotionStat.level1)
            StartCoroutine(ConvertObjectBool());
        if (other.CompareTag("SConverter") && enumStat == PotionStat.level2 || enumStat == PotionStat.level3)
            StartCoroutine(ConvertObjectBool());
        if (other.CompareTag("Crusher"))
            StackMechanic.Instance.CrashAllObjects();
        if (other.CompareTag("Finish"))
            FinishLine(other.gameObject);
        if (other.CompareTag("DConverter") && enumStat > PotionStat.level1)
        {
            PointText.Instance.CallPointMoneyText(CharacterManager.Instance.character, 1, false);
            StartCoroutine(ConvertDownObjectBool());
        }
    }

    private void FinishLine(GameObject other)
    {
        CharacterBar.Instance.BarPanel.SetActive(false);
        StartCoroutine(other.GetComponent<FinishConverterManager>().SellItem(gameObject, (int)enumStat, true));
        GameManager.Instance.enumStat = GameManager.GameStat.finish;

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
    private IEnumerator ConvertObjectBool()
    {
        if (!isConvert)
        {
            isConvert = true;
            enumStat++;
            PointText.Instance.CallPointMoneyText(gameObject, 1, true);
            StartCoroutine(StackMechanic.Instance.ObjectConverter(gameObject, (int)enumStat));
            yield return new WaitForSeconds(0.2f);
            isConvert = false;
        }
    }
}

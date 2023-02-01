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
    }

    private void FinishLine(GameObject other)
    {
        StartCoroutine(other.GetComponent<FinishConverterManager>().SellItem(gameObject, (int)enumStat, true));
        GameManager.Instance.enumStat = GameManager.GameStat.finish;

    }
    private IEnumerator ConvertObjectBool()
    {
        if (!isConvert)
        {
            isConvert = true;
            enumStat++;
            StartCoroutine(ConvertObject());
            StackMechanic.Instance.ObjectConverter(gameObject, (int)enumStat);
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator ConvertObject()
    {
        Vector3 tempScale = transform.localScale;
        transform.DOScale(transform.localScale * 1.3f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.DOScale(tempScale, 0.2f);
        yield return new WaitForSeconds(0.2f);
    }
}

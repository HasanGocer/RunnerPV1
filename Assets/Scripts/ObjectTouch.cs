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
            StartCoroutine(ConvertObjectBool());
        if (other.CompareTag("SConverter") && isActive && enumStat == PotionStat.level2 || enumStat == PotionStat.level3)
            StartCoroutine(ConvertObjectBool());
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
            StartCoroutine(ConvertObject());
            StackMechanic.Instance.ObjectConverter(gameObject, (int)enumStat);
            yield return new WaitForSeconds(0.2f);
            isConvert = false;
        }
    }

    private IEnumerator ConvertObject()
    {
        Vector3 tempScale = transform.localScale;
        transform.DOScale(transform.localScale * 1.1f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.DOScale(tempScale, 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.localScale = tempScale;
    }
}

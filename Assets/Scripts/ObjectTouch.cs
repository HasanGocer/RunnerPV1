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
        level3 = 3
    }

    public PotionStat enumStat = PotionStat.level1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            StartCoroutine(StackMechanic.Instance.AddNewObject(gameObject));
            tag = "Untagged";
        }
        if (other.CompareTag("Converter"))
        {
            StartCoroutine(ConvertObject());
        }
        if (other.CompareTag("FinishConverter") && enumStat != PotionStat.level3)
        {
            enumStat++;
            StackMechanic.Instance.ObjectConverter(gameObject, (int)enumStat);
        }
        if (other.CompareTag("Crusher"))
        {
            StackMechanic.Instance.CrashObjects(gameObject);
        }
    }

    private IEnumerator ConvertObject()
    {
        transform.DOScale(transform.localScale * 2, 0.3f);
        yield return new WaitForSeconds(0.4f);
        transform.DOScale(transform.localScale / 2, 0.3f);
        yield return new WaitForSeconds(0.4f);
    }
}

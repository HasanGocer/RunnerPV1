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

    bool isConvert = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            StartCoroutine(StackMechanic.Instance.AddNewObject(gameObject));
            other.tag = "Untagged";
        }
        if (other.CompareTag("Converter"))
        {
            StartCoroutine(ConvertObjectBool());
        }
        if (other.CompareTag("FinishConverter") && enumStat != PotionStat.level3)
        {
            StackMechanic.Instance.ObjectConverter(gameObject, (int)enumStat);
        }
        if (other.CompareTag("Crusher"))
        {
            StackMechanic.Instance.CrashObjects(gameObject);
        }
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
        transform.DOScale(transform.localScale * 1.3f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        transform.DOScale(tempScale, 0.3f);
        yield return new WaitForSeconds(0.4f);
    }
}

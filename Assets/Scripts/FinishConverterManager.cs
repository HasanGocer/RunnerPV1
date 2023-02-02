using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishConverterManager : MonoBehaviour
{
    [SerializeField] GameObject FinishPos;

    public IEnumerator SellItem(GameObject obj, int level, bool isLast)
    {
        if (!isLast) StackMechanic.Instance.ObjectSeller(obj);
        else FinishSystem.Instance.FinishTime();
        Vector3 firstPos = new Vector3(obj.transform.position.x, FinishPos.transform.position.y, FinishPos.transform.position.z);
        obj.transform.position = firstPos;
        obj.transform.DOMove(FinishPos.transform.position, 0.5f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(false);
        GameManager.Instance.addedMoney += level * Random.Range(4, 8);
    }
}

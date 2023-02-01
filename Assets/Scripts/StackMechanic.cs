using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackMechanic : MonoSingleton<StackMechanic>
{
    public List<GameObject> StackObjects = new List<GameObject>();
    public List<ObjectMovement> objectMovements = new List<ObjectMovement>();
    public float stackDistance;
    [SerializeField] private float _stackDelayTime, _scaleTime;
    [SerializeField] private int _velocityMaxRange;

    public IEnumerator AddNewObject(GameObject newObject)
    {
        ObjectMovement objectMovement = newObject.GetComponent<ObjectMovement>();

        newObject.tag = "Untagged";
        StackObjects.Add(newObject);
        objectMovements.Add(objectMovement);
        newObject.transform.position = new Vector3(CharacterManager.Instance.characterObjectPos.transform.position.x, CharacterManager.Instance.characterObjectPos.transform.position.y, CharacterManager.Instance.characterObjectPos.transform.position.z - (StackObjects.Count - 1) * stackDistance);
        objectMovement.isCrush = false;
        objectMovement.stackCount = objectMovements.Count;
        StartCoroutine(objectMovement.ObjectMove());

        foreach (GameObject item in StackObjects)
        {
            StartCoroutine(ObjectScale(item));
            yield return new WaitForSeconds(_stackDelayTime);
        }
    }

    public void ObjectSeller(GameObject tempObject)
    {
        int count = StackObjects.Count - 1;

        for (int i = count; i > 0; i--)
        {
            if (tempObject == StackObjects[i])
            {
                objectMovements[i].isCrush = true;
                objectMovements.RemoveAt(i);
                StackObjects.RemoveAt(i);
                break;
            }
            objectMovements[i].stackCount = i - 1;
        }
    }

    public void CrashObjects(GameObject tempObject, ObjectMovement objectMovement)
    {
        objectMovement.boxCollider.isTrigger = true;
        int count = StackObjects.Count - 1;

        for (int i = count; i > 0; i--)
        {
            ObjectThrow(StackObjects[i]);
            objectMovements[i].isCrush = true;
            objectMovements[i].stackCount = -1;
            StackObjects[i].tag = "Potion";
            StackObjects.RemoveAt(i);
            objectMovements.RemoveAt(i);
            if (tempObject == StackObjects[i]) break;
        }
    }

    public void ObjectConverter(GameObject obj, int enumStat)
    {
        obj.transform.GetChild(enumStat - 2).gameObject.SetActive(false);
        obj.transform.GetChild(enumStat - 1).gameObject.SetActive(true);
    }

    public void CrashAllObjects()
    {
        int count = StackObjects.Count - 1;

        for (int i = count; i > 0; i--)
        {
            ObjectThrow(StackObjects[i]);
            objectMovements[i].isCrush = true;
            objectMovements[i].stackCount = -1;
            ObjectThrow(StackObjects[i]);
            StackObjects[i].tag = "Potion";
            StackObjects.RemoveAt(i);
            objectMovements.RemoveAt(i);
        }
    }

    private void ObjectThrow(GameObject tempObject)
    {
        tempObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1 * _velocityMaxRange, _velocityMaxRange), 0, Random.Range(0, _velocityMaxRange));
    }
    private IEnumerator ObjectScale(GameObject tempObject)
    {
        Vector3 tempScale = tempObject.transform.localScale;
        tempObject.transform.DOScale(tempObject.transform.localScale * 1.1f, _scaleTime);
        yield return new WaitForSeconds(_scaleTime);
        tempObject.transform.DOScale(tempScale, _scaleTime);
        yield return new WaitForSeconds(_scaleTime);
    }
}

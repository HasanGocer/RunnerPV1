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

        StackObjects.Add(newObject);
        objectMovements.Add(objectMovement);
        newObject.transform.position = new Vector3(CharacterManager.Instance.characterObjectPos.transform.position.x, CharacterManager.Instance.characterObjectPos.transform.position.y, CharacterManager.Instance.characterObjectPos.transform.position.z - (StackObjects.Count - 1) * stackDistance);
        objectMovement.isCrush = false;
        objectMovement.stackCount = objectMovements.Count - 1;
        StartCoroutine(objectMovement.ObjectMove());

        foreach (GameObject item in StackObjects)
        {
            StartCoroutine(ObjectScale(item));
            yield return new WaitForSeconds(_stackDelayTime);
        }
    }

    public void CrashObjects(GameObject tempObject)
    {
        bool isCrash = false;

        for (int i = 0; i < StackObjects.Count; i++)
        {
            if (tempObject == StackObjects[i]) isCrash = true;
            if (isCrash)
            {
                ObjectThrow(StackObjects[i]);
                objectMovements[i].isCrush = true;
                objectMovements[i].stackCount = -1;
                StackObjects[i].tag = "Potion";
                StackObjects.RemoveAt(i);
                objectMovements.RemoveAt(i);
            }
        }
    }

    public void ObjectConverter(GameObject obj, int enumStat)
    {
        print(enumStat);
        obj.transform.GetChild(enumStat - 2).gameObject.SetActive(false);
        obj.transform.GetChild(enumStat - 1).gameObject.SetActive(true);

    }

    public void CrashAllObjects(GameObject tempObject)
    {
        for (int i = 0; i < StackObjects.Count; i++)
        {
            ObjectThrow(StackObjects[i]);
            objectMovements[i].isCrush = true;
            objectMovements[i].stackCount = -1;
            ObjectThrow(StackObjects[i]);
            StackObjects[i].tag = "Potion";
        }

        objectMovements.Clear();
        StackObjects.Clear();
    }

    private void ObjectThrow(GameObject tempObject)
    {
        tempObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1 * _velocityMaxRange, _velocityMaxRange), 0, Random.Range(0, _velocityMaxRange));
    }
    private IEnumerator ObjectScale(GameObject tempObject)
    {
        Vector3 tempScale = tempObject.transform.localScale;
        tempObject.transform.DOScale(tempObject.transform.localScale * 1.3f, _scaleTime);
        yield return new WaitForSeconds(_scaleTime + _stackDelayTime);
        tempObject.transform.DOScale(tempScale, _scaleTime);
        yield return new WaitForSeconds(_scaleTime + _stackDelayTime);
    }
}

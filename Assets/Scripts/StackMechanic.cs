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
    bool isCrush;
    bool isJump;

    public IEnumerator AddNewObject(GameObject newObject)
    {
        ObjectMovement objectMovement = newObject.GetComponent<ObjectMovement>();

        objectMovement.objectTouch.isActive = true;
        newObject.tag = "Untagged";
        StackObjects.Add(newObject);
        objectMovements.Add(objectMovement);
        objectMovement.isCrush = false;
        objectMovement.stackCount = objectMovements.Count;
        StartCoroutine(objectMovement.ObjectMove());

        if (!isJump)
        {
            isJump = true;
            for (int i = 0; i < StackObjects.Count; i++)
            {
                if (objectMovements[i] != null)
                {
                    StartCoroutine(ObjectScale(StackObjects[i]));
                    yield return new WaitForSeconds(_stackDelayTime);
                }
                else break;
            }
            isJump = false;
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
        if (!isCrush)
        {
            isCrush = true;
            GameObject freeObject;
            objectMovement.boxCollider.isTrigger = true;
            int count = StackObjects.Count - 1;
            StartCoroutine(ParticalSystem.Instance.CallBreakerPartical(tempObject));
            SoundSystem.Instance.CallGlassBreak();

            for (int i = count; i > 0; i--)
            {
                if (objectMovements[i] != null)
                {
                    StartCoroutine(ObjectThrow(StackObjects[i]));
                    objectMovements[i].isCrush = true;
                    objectMovements[i].stackCount = -1;
                    objectMovements[i].boxCollider.isTrigger = true;
                    objectMovements[i].objectTouch.isActive = false;
                    StackObjects[i].tag = "Potion";
                    freeObject = StackObjects[i];
                    StackObjects.RemoveAt(i);
                    objectMovements.RemoveAt(i);
                    if (tempObject == freeObject) break;
                }
                else break;
            }
            isCrush = false;
        }
    }

    public void ObjectConverter(GameObject obj, int enumStat)
    {
        obj.transform.GetChild(enumStat - 2).gameObject.SetActive(false);
        obj.transform.GetChild(enumStat - 1).gameObject.SetActive(true);
    }

    public void CrashAllObjects()
    {
        if (!isCrush)
        {
            isCrush = true;
            int count = StackObjects.Count - 1;

            for (int i = count; i > 0; i--)
            {
                StartCoroutine(ObjectThrow(StackObjects[i]));
                objectMovements[i].isCrush = true;
                objectMovements[i].stackCount = -1;
                objectMovements[i].boxCollider.isTrigger = true;
                objectMovements[i].objectTouch.isActive = false;
                StackObjects[i].tag = "Potion";
                StackObjects.RemoveAt(i);
                objectMovements.RemoveAt(i);
            }
            isCrush = false;
        }

    }

    private IEnumerator ObjectThrow(GameObject tempObject)
    {
        Rigidbody rigidbody = tempObject.GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(Random.Range(-1 * _velocityMaxRange, _velocityMaxRange), 0, Random.Range(0, _velocityMaxRange));
        yield return new WaitForSeconds(1);
        rigidbody.isKinematic = true;
        yield return new WaitForSeconds(0.1f);
        rigidbody.isKinematic = false;
    }
    private IEnumerator ObjectScale(GameObject tempObject)
    {
        tempObject.transform.DOScale(tempObject.transform.localScale * 1.1f, _scaleTime);
        yield return new WaitForSeconds(_scaleTime);
        tempObject.transform.DOScale(new Vector3(1, 1, 1), _scaleTime);
        tempObject.transform.localScale = new Vector3(1, 1, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoSingleton<ObjectsManager>
{
    public List<GameObject> ObjectsPotions = new List<GameObject>();
    public List<GameObject> ObjectsConverters = new List<GameObject>();
    public List<GameObject> ObjectsPotionPos = new List<GameObject>();
    public List<GameObject> ObjectsConverterPos = new List<GameObject>();

    public void ObjectsPlacement()
    {
        for (int i = 0; i < ObjectsConverterPos.Count; i++)
            Instantiate(ObjectsConverters[Random.Range(0, ObjectsConverters.Count)], ObjectsConverterPos[i].transform.position, ObjectsConverterPos[i].transform.rotation);
        for (int i = 0; i < ObjectsPotionPos.Count; i++)
            Instantiate(ObjectsPotions[Random.Range(0, ObjectsPotions.Count)], ObjectsPotionPos[i].transform.position, ObjectsPotionPos[i].transform.rotation);
    }
}

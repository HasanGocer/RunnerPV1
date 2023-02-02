using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoSingleton<ObjectsManager>
{
    public List<GameObject> ObjectsPotions = new List<GameObject>();
    public List<GameObject> ObjectsConverters = new List<GameObject>();
    public List<GameObject> ObjectsPotionPos = new List<GameObject>();
    public List<GameObject> ObjectsConverterPos = new List<GameObject>();
    public GameObject potionConverterGO;
    public GameObject potionConverterPos;


    public void ObjectsPlacement()
    {
        GameObject obj = Instantiate(potionConverterGO, potionConverterPos.transform.position, potionConverterPos.transform.rotation);
        obj.SetActive(true);
        for (int i = 0; i < ObjectsConverterPos.Count; i++)
        {
            obj = Instantiate(ObjectsConverters[Random.Range(0, ObjectsConverters.Count)], ObjectsConverterPos[i].transform.position, ObjectsConverterPos[i].transform.rotation);
            obj.SetActive(true);
        }
        for (int i = 0; i < ObjectsPotionPos.Count; i++)
        {
            obj = Instantiate(ObjectsPotions[Random.Range(0, ObjectsPotions.Count)], ObjectsPotionPos[i].transform.position, ObjectsPotionPos[i].transform.rotation);
            obj.SetActive(true);
        }
    }
}

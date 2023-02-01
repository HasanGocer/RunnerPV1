using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public List<GameObject> Objects = new List<GameObject>();

    public List<GameObject> ObjectsPos = new List<GameObject>();

    public void ObjectsPlacement()
    {
        for (int i = 0; i < ObjectsPos.Count; i++)
        {
            Instantiate(Objects[Random.Range(0, Objects.Count)], ObjectsPos[i].transform.position, ObjectsPos[i].transform.rotation);
        }
    }
}

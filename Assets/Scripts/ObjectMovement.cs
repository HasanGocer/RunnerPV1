using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public int stackCount = -1;
    public bool isCrush = false;

    public IEnumerator ObjectMove()
    {
        yield return null;
        while (!isCrush)
        {
            transform.position = new Vector3(
           Mathf.Lerp(transform.position.x, CharacterManager.Instance.characterObjectPos.transform.position.x, Time.deltaTime),
             transform.position.y,
              CharacterManager.Instance.characterObjectPos.transform.position.z + stackCount * StackMechanic.Instance.stackDistance);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}

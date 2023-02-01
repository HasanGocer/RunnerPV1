using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public int stackCount = -1;
    public bool isCrush = false;
    public BoxCollider boxCollider;

    public IEnumerator ObjectMove()
    {
        boxCollider.isTrigger = false;

        yield return null;
        while (!isCrush && GameManager.Instance.enumStat == GameManager.GameStat.start)
        {

            transform.position = new Vector3(
           Mathf.Lerp(transform.position.x, StackMechanic.Instance.StackObjects[stackCount - 2].transform.position.x, Time.deltaTime * 12),
             transform.position.y,
              CharacterManager.Instance.characterObjectPos.transform.position.z + stackCount * StackMechanic.Instance.stackDistance);
            yield return new WaitForSeconds(Time.deltaTime / 3);
        }
    }
}

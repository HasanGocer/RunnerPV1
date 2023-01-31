using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            StartCoroutine(StackMechanic.Instance.AddNewObject(other.gameObject));
            other.tag = "Untagged";
        }
        if (other.CompareTag("Crusher"))
        {
            StackMechanic.Instance.CrashObjects(gameObject);
        }
        if (other.CompareTag("Finish"))
        {
            GameManager.Instance.enumStat = GameManager.GameStat.finish;
        }
    }
}

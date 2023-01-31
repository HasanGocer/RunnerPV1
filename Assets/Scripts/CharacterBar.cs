using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBar : MonoSingleton<CharacterBar>
{
    public GameObject BarPanel;
    [SerializeField] private Image bar;
    [SerializeField] private float nowDistance, maxDistance;

    public void BarUpdate()
    {
        CharacterManager characterManager = CharacterManager.Instance;
        maxDistance = Vector3.Distance(characterManager.character.transform.position, characterManager.FinishPos.transform.position);

        StartCoroutine(BarUpdateIenumurator());
    }

    private IEnumerator BarUpdateIenumurator()
    {
        CharacterManager characterManager = CharacterManager.Instance;
        yield return null;
        float temp = 0;
        while (true)
        {
            temp += Time.deltaTime;
            nowDistance = Vector3.Distance(characterManager.character.transform.position, characterManager.FinishPos.transform.position);
            bar.fillAmount = Mathf.Lerp(nowDistance, maxDistance, temp);
            yield return new WaitForEndOfFrame();
            if (bar.fillAmount == 1)
            {
                break;
            }
        }
    }
}

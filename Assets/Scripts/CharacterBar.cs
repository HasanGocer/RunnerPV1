using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBar : MonoSingleton<CharacterBar>
{
    public GameObject BarPanel;
    [SerializeField] private Image bar;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] private float nowDistance, maxDistance;
    [SerializeField] TMP_Text nowLevel, nextLevel;

    public void StartBar()
    {
        nowLevel.text = GameManager.Instance.level.ToString();
        nextLevel.text = (GameManager.Instance.level + 1).ToString();
    }

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
            float lerpCount = (maxDistance - nowDistance) / maxDistance;
            rectTransform.anchoredPosition = new Vector2((rectTransform.sizeDelta.x * bar.fillAmount * 4.3f) - 250, rectTransform.anchoredPosition.y);

            bar.fillAmount = lerpCount;
            yield return new WaitForEndOfFrame();
            if (bar.fillAmount == 1 || 1 > Vector3.Distance(characterManager.character.transform.position, characterManager.FinishPos.transform.position))
                break;
        }
    }
}

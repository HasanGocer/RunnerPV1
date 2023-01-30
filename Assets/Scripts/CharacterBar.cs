using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    public bool isLive;

    public void BarUpdate(int max, int count, int down)
    {
        float nowBar = count / max;
        float afterBar = (count - down) / max;
        StartCoroutine(BarUpdateIenumurator(nowBar, afterBar));
    }

    private IEnumerator BarUpdateIenumurator(float start, float finish)
    {
        yield return null;
        float temp = 0;
        while (true)
        {
            temp += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(start, finish, temp);
            yield return new WaitForEndOfFrame();
            if (bar.fillAmount == finish)
            {
                if (isLive)
                {
                    RivalControl();
                }
                else
                {
                    Buttons.Instance.failPanel.SetActive(true);
                    MoneySystem.Instance.MoneyTextRevork(GameManager.Instance.addedMoney);
                }
                break;
            }
        }
    }

    private void RivalControl()
    {
        //�l�m
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoSingleton<ParticalSystem>
{
    [SerializeField] private int _OPPotionParticalCount, _OPConvertParticalCount, _OPBreakerParticalCount,_OPFinishParticalCount;
    [SerializeField] float _potionParticalTime, _convertParticalTime, _breakerParticalTime,_finishParticalTime;

    public IEnumerator CallPotionPartical(GameObject pos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPPotionParticalCount);
        obj.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_potionParticalTime);
        ObjectPool.Instance.AddObject(_OPPotionParticalCount, obj);
    }
    public IEnumerator CallConvertPartical(GameObject pos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPConvertParticalCount);
        obj.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_convertParticalTime);
        ObjectPool.Instance.AddObject(_OPConvertParticalCount, obj);
    }

    public IEnumerator CallBreakerPartical(GameObject pos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPBreakerParticalCount);
        obj.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_breakerParticalTime);
        ObjectPool.Instance.AddObject(_OPBreakerParticalCount, obj);
    }

    public IEnumerator CallFinishPartical(GameObject pos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPBreakerParticalCount);
        obj.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_breakerParticalTime);
        ObjectPool.Instance.AddObject(_OPBreakerParticalCount, obj);
    }
}

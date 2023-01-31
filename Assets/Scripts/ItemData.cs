using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    [System.Serializable]
    public class Field
    {
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    public void AwakeID()
    {
        /* field.objectTypeCount = standart.objectTypeCount + (factor.objectTypeCount * constant.objectTypeCount);
         fieldPrice.objectTypeCount = fieldPrice.objectTypeCount * factor.objectTypeCount;

         if (factor.objectTypeCount > maxFactor.objectTypeCount)
         {
             factor.objectTypeCount = maxFactor.objectTypeCount;
             field.objectTypeCount = standart.objectTypeCount + (factor.objectTypeCount * constant.objectTypeCount);
             fieldPrice.objectTypeCount = fieldPrice.objectTypeCount / (factor.objectTypeCount - 1);
             fieldPrice.objectTypeCount = fieldPrice.objectTypeCount * factor.objectTypeCount;
         }
         */
    }

    /*public void SetObjectTypeCount()
    {
        factor.objectTypeCount++;

        field.objectTypeCount = standart.objectTypeCount + (factor.objectTypeCount * constant.objectTypeCount);
        fieldPrice.objectTypeCount = fieldPrice.objectTypeCount / (factor.objectTypeCount - 1);
        fieldPrice.objectTypeCount = fieldPrice.objectTypeCount * factor.objectTypeCount;

        if (factor.objectTypeCount > maxFactor.objectTypeCount)
        {
            factor.objectTypeCount = maxFactor.objectTypeCount;
            field.objectTypeCount = standart.objectTypeCount + (factor.objectTypeCount * constant.objectTypeCount);
            fieldPrice.objectTypeCount = fieldPrice.objectTypeCount / (factor.objectTypeCount - 1);
            fieldPrice.objectTypeCount = fieldPrice.objectTypeCount * factor.objectTypeCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }*/

}

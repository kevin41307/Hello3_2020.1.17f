using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelpers 
{
    public static Transform DeepFind(this Transform parent, string targetName)
    {
        foreach (Transform child in parent)
        {
            if(child.name.Equals(targetName))
            {
                return child;
            }
            else
            {
                Transform tempTrans = DeepFind(child, targetName);
                if (tempTrans != null)
                    return tempTrans;
            }
        }
        return null;
    }
}

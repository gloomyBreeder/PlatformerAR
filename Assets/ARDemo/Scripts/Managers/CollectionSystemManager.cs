using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSystemManager : BasicManager<CollectionSystemManager>
{
    private int _count = 0;

    public void AddToCollection()
    {
        _count += 1;
        Debug.Log("Collection items count " + _count);
    }

    public void RemoveFromCollection()
    {
        _count -= 1;
        Debug.Log("Collection items count " + _count);
        if (_count == 0)
        {
            Debug.Log("all coins are collected!");
            ARUIManager.instance.ShowMessage(ARUIManager.PanelConditions.End);
            PortalManager.instance.OpenPortal();
        }
    }
}

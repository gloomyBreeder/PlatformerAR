using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItem : MonoBehaviour
{
    private CollectionSystemManager _manager;
    void Start()
    {
        _manager = CollectionSystemManager.instance;
        _manager.AddToCollection();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MainCharacterController>() != null)
        {
            _manager.RemoveFromCollection();
            Destroy(gameObject);
        }
    }
}

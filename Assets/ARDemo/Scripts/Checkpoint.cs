using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string ID;
    private CheckpointManager _manager;

    private void Start()
    {
        _manager = CheckpointManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MainCharacterController>() != null && !_manager.IsInCheckpoints(this))
        {
            Debug.Log("saved your position!");
            _manager.AddToCheckpoints(this);
            _manager.SetLastCheckpointPosition(transform.position);
        }
    }
}

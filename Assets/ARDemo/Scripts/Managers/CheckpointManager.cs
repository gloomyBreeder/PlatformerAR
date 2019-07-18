using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : BasicManager<CheckpointManager>
{
    private Vector3 _lastCheckpointPos;
    private List<Checkpoint> _checkpoints = new List<Checkpoint>();

    public bool IsInCheckpoints(Checkpoint check)
    {
        if (_checkpoints.Find(checkpoint => checkpoint.ID == check.ID) != null)
            return true;
        return false;
    }
    public void SetLastCheckpointPosition(Vector3 pos)
    {
        _lastCheckpointPos = pos;
    }

    public void AddToCheckpoints(Checkpoint check)
    {
        _checkpoints.Add(check);
    }

    /*private void RemoveFromCheckpoints(Checkpoint check)
    {
        checkpoints.Remove(check);
    }*/

    public Vector3 GetLastCheckpointPosition()
    {
        return _lastCheckpointPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    private List<FlowPoint> flowSet;

    public PlayerData ()
    {
        this.flowSet = new List<FlowPoint>();
    }

    public void AddTheNewFlowPoint(int second_point, int score_point)
    {
        FlowPoint newPoint = new FlowPoint(second_point, score_point);
        flowSet.Add(newPoint);
    }

    public List<FlowPoint> GetFlowSet()
    {
        return this.flowSet;
    }

    public FlowPoint GetTheLastFlowPoint()
    {
        return this.flowSet[this.flowSet.Count - 1];
    }

    public void UpdateTheLastFlowPoint(int new_score_point)
    {
        this.flowSet[this.flowSet.Count - 1].UpdateScorePoint(new_score_point);
    }
}

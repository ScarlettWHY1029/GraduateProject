using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlowPoint
{
    [SerializeField]
    private int second_point;
    [SerializeField]
    private int score_point;
    
    public FlowPoint(int second_point, int score_point)
    {
        this.second_point = second_point;
        this.score_point = score_point;
    }

    public int GetSecondPoint()
    {
        return this.second_point;
    }

    public int GetScorePoint()
    {
        return this.score_point;
    }

    public void UpdateScorePoint(int new_score_point)
    {
        this.score_point = new_score_point;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GoalPoint> _points = new List<GoalPoint>();
    int _pointCounter = 0;

    void Start()
    {
        _points.Clear();

        var pointsGO = GameObject.FindGameObjectsWithTag("Goal Point");
        foreach(var go in pointsGO) {
            var point = go.GetComponent<GoalPoint>();
            point.SetManager(this);
            _points.Add(point);
        }
    }

    internal void TriggerPoint(bool active) {
        if (active)
            _pointCounter++;
        else
            _pointCounter--;

        if (_pointCounter == _points.Count) {
            Debug.LogWarning("YOU WIN");
        }
    }
}

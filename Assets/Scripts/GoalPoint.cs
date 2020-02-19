using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    GameManager _manager;
    internal void SetManager(GameManager manager) {
        _manager = manager;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        _manager.TriggerPoint(true);
    }

    void OnTriggerExit2D(Collider2D collision) {
        _manager.TriggerPoint(false);
    }
}

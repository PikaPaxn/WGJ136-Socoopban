using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    bool hasMove = false;

    void LateUpdate() {
        hasMove = false;
    }

    void OnMove(InputValue value) {
        var v2 = value.Get<Vector2>();
        OnMove(v2);
    }

    public bool OnMove(Vector2 direction) {
        if (hasMove)
            return true;

        //Define one direction
        var absX = Mathf.Abs(direction.x);
        var absY = Mathf.Abs(direction.y);

        if (absX > absY)
            direction.y = 0;
        else
            direction.x = 0;

        //Check if there are obstacles
        var hit = Physics2D.Raycast(transform.position, direction, 1f);

        if (hit.collider != null) {
            Debug.Log("Obstacle found!", hit.collider.gameObject);

            //Box
            var box = hit.collider.GetComponent<Box>();
            if (box != null) {
                if (box.Move(direction.normalized)) {
                    transform.Translate(direction.normalized);
                    hasMove = true;
                }
            }

            //Player
            var player = hit.collider.GetComponent<Player>();
            if (player != null) {
                if (player.OnMove(direction.normalized)) {
                    transform.Translate(direction.normalized);
                    hasMove = true;
                }
            }
        } else {
            //Move in the direction
            transform.Translate(direction.normalized);
            hasMove = true;
        }

        return hasMove;
    }
}

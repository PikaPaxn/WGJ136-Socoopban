using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool Move(Vector2 direction) {
        var hit = Physics2D.Raycast(transform.position, direction, 1f);

        if (hit.collider != null) {
            //Debug.Log("Obstacle collide against another!", gameObject);
            return false;
        } else {
            transform.Translate(direction);
            return true;
        }
    }
}

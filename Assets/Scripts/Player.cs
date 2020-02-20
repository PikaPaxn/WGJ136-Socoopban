using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    bool _hasMove = false;
    Animator _animator;

    void Start() {
        _animator = GetComponent<Animator>();
    }

    void LateUpdate() {
        _hasMove = false;
    }

    void OnMove(InputValue value) {
        var v2 = value.Get<Vector2>();
        OnMove(v2);
    }

    internal bool OnMove(Vector2 direction) {
        if (GameManager.hasWin)
            return false;

        if (_hasMove)
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
            //Debug.Log("Obstacle found!", hit.collider.gameObject);

            //Box
            var box = hit.collider.GetComponent<Box>();
            if (box != null) {
                if (box.Move(direction.normalized)) {
                    _hasMove = true;
                }
            }

            //Player
            var player = hit.collider.GetComponent<Player>();
            if (player != null) {
                if (player.OnMove(direction.normalized)) {
                    _hasMove = true;
                }
            }
        } else {
            //Move in the direction
            _hasMove = true;
        }

        if (_hasMove) {
            //Left or Right
            if (direction.y == 0) {
                if (direction.x < 0) {
                    _animator.SetTrigger("Left");
                } else if (direction.x > 0) {
                    _animator.SetTrigger("Right");
                }
            
            //Up or Down
            } else {
                if (direction.y < 0) {
                    _animator.SetTrigger("Down");
                } else if (direction.y > 0) {
                    _animator.SetTrigger("Up");
                }
            }

            //Move
            transform.Translate(direction.normalized);
        }

        return _hasMove;
    }
}

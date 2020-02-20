using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    internal static bool hasWin = false;

    List<GoalPoint> _points = new List<GoalPoint>();
    int _pointCounter = 0;

    List<Player> _players = new List<Player>();

    [SerializeField] string nextLevelName;
    [SerializeField] UnityEvent onWin;

    void Start()
    {
        hasWin = false;
        _points.Clear();

        var pointsGO = GameObject.FindGameObjectsWithTag("Goal Point");
        foreach(var go in pointsGO) {
            var point = go.GetComponent<GoalPoint>();
            point.SetManager(this);
            _points.Add(point);
        }

        _players.Clear();
        var playersGO = GameObject.FindGameObjectsWithTag("Player");
        foreach (var go in playersGO) {
            var player = go.GetComponent<Player>();
            _players.Add(player);
        }
    }

    internal void TriggerPoint(bool active) {
        if (active)
            _pointCounter++;
        else
            _pointCounter--;

        if (_pointCounter == _points.Count) {
            Debug.LogWarning("YOU WIN");
            hasWin = true;
            onWin.Invoke();
        }
    }

    //Buttons Helper
    public void MovePlayersUp()    { MovePlayers(Vector2.up);    }
    public void MovePlayersDown()  { MovePlayers(Vector2.down);  }
    public void MovePlayersLeft()  { MovePlayers(Vector2.left);  }
    public void MovePlayersRight() { MovePlayers(Vector2.right); }

    void MovePlayers(Vector2 dir) {
        foreach (var player in _players) {
            player.OnMove(dir);
        }
    }

    public void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void NextLevel() {
        SceneManager.LoadScene(nextLevelName);
    }
}

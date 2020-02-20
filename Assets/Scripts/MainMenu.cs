using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string levelName = "Base Level";

    public void LoadLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}

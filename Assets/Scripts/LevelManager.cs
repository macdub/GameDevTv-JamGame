using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1.0f;
    public void LoadGame(string sceneLevel)
    {
        // eventually add parameter to use the same function to load different level
        StartCoroutine(WaitAndLoad(sceneLevel, loadDelay));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("Scenes/GameOver", loadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    private static IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            // move player to start location
            var startLocation = GameObject.Find("StartPoint").transform.position;
            var player = GameObject.Find("StartingPlayer");
            var input = player.gameObject.GetComponent<PlayerInput>();
            input.DeactivateInput();
            player.gameObject.GetComponent<PlayerMovement>().MoveTo(startLocation);
        }
        else
        {
            // do the flying dutchman spawn
        }
    }
}

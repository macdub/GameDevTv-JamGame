using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private LevelManager _levelManager;
    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        _levelManager.LoadGame("Scenes/ToBeContinued");
    }
}

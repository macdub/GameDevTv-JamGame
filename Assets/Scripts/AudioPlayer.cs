using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] [Range(0f, 1f)] private float fxVolume = 1f;

    private Vector3 _cameraPos;
    private static AudioPlayer _instance;

    public AudioPlayer Instance => _instance;

    private void Awake()
    {
        if (Camera.main != null) _cameraPos = Camera.main.transform.position;
        ManageSingleTon();
    }
    
    private void ManageSingleTon()
    {
        if(_instance is not null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        if (shootingClip is null) return;
        PlayClip(shootingClip, fxVolume);
    }

    public void PlayHitClip()
    {
        if (hitClip is null) return;
        PlayClip(hitClip, fxVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, _cameraPos, volume);
    }
}

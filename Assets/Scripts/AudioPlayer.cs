using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip hpClip;
    [SerializeField] private AudioClip crewClip;
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

    public void PlayDropClip(ItemType type)
    {
        var clip = type switch
        {
            ItemType.Health => hpClip,
            ItemType.Sailor => crewClip,
            ItemType.Marines => crewClip,
            ItemType.Spacemarines => crewClip,
            _ => null
        };

        if (clip is null) return;
        PlayClip(clip, fxVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, _cameraPos, volume);
    }
}

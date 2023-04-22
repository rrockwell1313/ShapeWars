using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip buttonClick;
    public AudioClip shapeSpawn;
    public AudioClip shapeCollision;
    public AudioClip shapeDestruction;

    private AudioSource backgroundMusicSource;
    private AudioSource buttonClickSource;
    private AudioSource shapeSpawnSource;
    private AudioSource shapeCollisionSource;
    private AudioSource shapeDestructionSource;

    void Start()
    {
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.volume = 0.3f;
        buttonClickSource = gameObject.AddComponent<AudioSource>();
        shapeSpawnSource = gameObject.AddComponent<AudioSource>();
        shapeSpawnSource.volume = 1.0f;
        shapeCollisionSource = gameObject.AddComponent<AudioSource>();
        shapeDestructionSource = gameObject.AddComponent<AudioSource>();

        backgroundMusicSource.clip = backgroundMusic;
        buttonClickSource.clip = buttonClick;
        shapeSpawnSource.clip = shapeSpawn;
        shapeCollisionSource.clip = shapeCollision;
        shapeDestructionSource.clip = shapeDestruction;

        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void SetGameOver()
    {
        backgroundMusicSource.volume = 0.1f;
    }

    public void PlayButtonClick()
    {
        buttonClickSource.Play();
    }

    public void PlayShapeSpawn()
    {
        shapeSpawnSource.pitch = Random.Range(1f, 3f);
        shapeSpawnSource.Play();
    }

    public void PlayShapeCollision()
    {
        if (!shapeCollisionSource.isPlaying)
        {
            shapeCollisionSource.pitch = Random.Range(1f, 3f);
            shapeCollisionSource.Play();
        }
    }

    public void PlayShapeDestruction()
    {
        shapeDestructionSource.pitch = Random.Range(1f, 3f);
        shapeDestructionSource.Play();
    }
}


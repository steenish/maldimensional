using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

// Inspired by Brackey's audio manager.
public class AudioManager : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private Sound[] sounds;
#pragma warning restore

    private static AudioManager instance;

    private void OnEnable() {
        SceneManager.sceneLoaded += PlayNewSceneSounds;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= PlayNewSceneSounds;
    }

    void Awake() {
        // Handle asset loader instancing between scene loads.
        // If there is no instance, let this be the new instance, otherwise, destroy this object.
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        // If this object was set as the instance, make sure it is not destroyed on scene loads.
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play(string name) {
        Sound sound = Array.Find(sounds, e => e.name == name);

        if (sound != null) {
            sound.source.Play();
        } else {
            Debug.LogWarning("Audio clip " + name + " not found. No audio played.");
        }
    }

    public void Stop(string name) {
        Sound sound = Array.Find(sounds, e => e.name == name);

        if (sound != null) {
            sound.source.Stop();
        } else {
            Debug.LogWarning("Audio clip " + name + " not found. No audio stopped.");
        }
    }

    private void PlayNewSceneSounds(Scene scene, LoadSceneMode mode) {
        switch (scene.name) {
            case "MainScene":
                break;
            case "StartScene":
                break;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

    [SerializeField]
    private string _name;
    public string name { get => _name; private set => _name = value; }

    [SerializeField]
    private AudioClip _audioClip;
    public AudioClip clip { get => _audioClip; private set => _audioClip = value; }

    [SerializeField]
    private bool _loop;
    public bool loop { get => _loop; private set => _loop = value; }

    [SerializeField]
    private float _volume;
    public float volume { get => _volume; private set => _volume = value; }

    [SerializeField]
    private float _pitch;
    public float pitch { get => _pitch; private set => _pitch = value; }

    public AudioSource source { get; set; }
}
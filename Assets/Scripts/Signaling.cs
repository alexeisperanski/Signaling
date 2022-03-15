using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _description;

    private float _timeAfterStart = 0;

    private void StartPlay()
    {
        _timeAfterStart += Time.deltaTime;
        _audio.volume = _timeAfterStart / _description;
    }
}

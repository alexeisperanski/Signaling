using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _timeForChangeVolume;
    [SerializeField] private Door _door;

    private Coroutine _signalChange = null;

    private void OnEnable()
    {
        _door.IsOpen += OnIsDoor;
    }

    private void OnDisable()
    {
        _door.IsOpen -= OnIsDoor;
    }

    private void OnIsDoor(bool flag)
    {
        if (_signalChange != null)
            StopCoroutine(_signalChange);

        _signalChange = StartCoroutine(SignalVolume(flag));
    }

    private IEnumerator SignalVolume(bool flag)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        int targetVolume = flag ? 1 : 0;

        if (_audioSource.isPlaying == false && flag == true)
            _audioSource.Play();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, Time.deltaTime / _timeForChangeVolume) ;
            yield return waitForFixedUpdate;
        }

        if (flag == false)
            _audioSource.Stop();
    }
}


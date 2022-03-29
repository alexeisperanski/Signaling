using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm: MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _timeForChangeVolume;
    
    private bool _isAlarm = false;
    private Coroutine _signalChange = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DetectThief(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DetectThief(collision, false);
    }

    private void DetectThief(Collider2D collision, bool isEnter)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _isAlarm = isEnter;

            if (_signalChange != null)
                StopCoroutine(_signalChange);

            _signalChange = StartCoroutine(SignalVolume(_isAlarm ? 1 : 0));
        }
    }

    private IEnumerator SignalVolume(int targetVolume)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();

        if (_audioSource.isPlaying == false && _isAlarm == true)
            _audioSource.Play();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, Time.deltaTime / _timeForChangeVolume) ;
            yield return waitForFixedUpdate;
        }

        if (_isAlarm == false)
            _audioSource.Stop();
    }
}


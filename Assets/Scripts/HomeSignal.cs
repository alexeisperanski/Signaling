using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HomeSignal : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private AudioSource audioSource;
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

            _signalChange = StartCoroutine(SignalVolume());
        }
    }

    private IEnumerator SignalVolume()
    {
        if (audioSource.isPlaying == false && _isAlarm == true)
            audioSource.Play();

        int targetVolume = _isAlarm ? 1 : 0;

        while (audioSource.volume != targetVolume)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, Time.deltaTime / _timeForChangeVolume) ;
            yield return new WaitForFixedUpdate();
        }

        if (_isAlarm == false)
            audioSource.Stop();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorSignal : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _description;
    
    private bool _isAlarm = false;
    private Coroutine _signalChange = null;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        RaycastHit2D[] _results = new RaycastHit2D[1];
        int number = _rigidbody2D.Cast(transform.up, _results, 1);
        _isAlarm = false;

        for (int i = 0; i < number; i++)
        {
            if (_results[i].collider.TryGetComponent<Thief>(out Thief thief))
            {
                _isAlarm = true;

                if(_signalChange != null)
                    StopCoroutine(_signalChange);

                _signalChange = StartCoroutine(OnSignal());
            }
        }

        if (_isAlarm == false)
        {

            if (_signalChange != null)
                StopCoroutine(_signalChange);

            _signalChange = StartCoroutine(OffSignal());
        }
    }

    private IEnumerator OnSignal()
    {
        if(_audioSource.isPlaying == false)
            _audioSource.Play();

        while (_audioSource.volume < 1) 
        {
            _audioSource.volume += Time.deltaTime / _description;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator OffSignal()
    {
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime / _description;
            yield return new WaitForFixedUpdate();
        }

        _audioSource.Stop();
    }
}


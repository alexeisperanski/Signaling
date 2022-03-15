using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private UnityEvent _penetrated;

    private bool _isAlarm = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            if (thief.transform.position.y >= transform.position.y)
                _isAlarm = true;
            else
                _isAlarm = false;
        }
    }

    private void OnValidate()
    {
    }
}


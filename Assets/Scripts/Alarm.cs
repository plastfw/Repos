using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _duration;

    private float _runningTime;
    private int _triggerCount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _alarmSound.Play();

        if (collision.TryGetComponent<Player>(out Player player))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeVolume(_triggerCount));
        }
    }


    private IEnumerator ChangeVolume(int count)
    {
        float _targetVolume = 1f;
        var waitForSecond = new WaitForSeconds(0.01f);

        _runningTime += Time.deltaTime;
        float normilizeRinningTime = _runningTime / _duration;
        _targetVolume *= normilizeRinningTime;

        _runningTime = 0;

        if (count == 1)
        {
            _triggerCount++;

            while (_alarmSound.volume < 1) 
            {
                _alarmSound.volume += _targetVolume;
                yield return null;
            }
        }
        if (count == 2)
        {
            _triggerCount--;

            while (_alarmSound.volume > 0)
            {
                _alarmSound.volume -= _targetVolume;
                yield return null;
            }
        }
    }
}
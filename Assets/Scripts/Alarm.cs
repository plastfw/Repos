using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _duration;

    private float _runningTime;
    private  bool _inside = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _alarmSound.Play();

        if (collision.TryGetComponent<Player>(out Player player))
        {
            StopCoroutine(ChangeVolume(_inside));
            StartCoroutine(ChangeVolume(_inside));
        }
    }


    private IEnumerator ChangeVolume(bool inside)
    {
        float _targetVolume = 1f;

        _runningTime += Time.deltaTime;
        float normilizeRinningTime = _runningTime / _duration;
        _targetVolume *= normilizeRinningTime;

        _runningTime = 0;

        if (inside)
        {
            _inside = false;

            while (_alarmSound.volume < 1) 
            {
                _alarmSound.volume += _targetVolume;
                yield return null;
            }
        }
        if (!inside)
        {
            _inside = true;

            while (_alarmSound.volume > 0)
            {
                _alarmSound.volume -= _targetVolume;
                yield return null;
            }
        }
    }
}
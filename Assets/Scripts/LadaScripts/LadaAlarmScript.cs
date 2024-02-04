using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadaAlarmScript : MonoBehaviour
{
    [SerializeField] private float _alarmDuration;

    [SerializeField] private GameObject _lightParent;

    [SerializeField] private GameObject _noiseCanvas;

    [SerializeField] private AudioSource _alarmSource;
    [SerializeField] private AudioSource _glassBreakSource;

    [SerializeField] private float _alarmRadius;

    private float _alarmEnd;

    private bool _alarmStopped;

    private Transform _mainCameraTransform;
    private Vector3 _initialNoiseCanvasScale;

    private void Start()
    {
        _alarmEnd = Mathf.Infinity;

        _mainCameraTransform = Camera.main.transform;

        _initialNoiseCanvasScale = _noiseCanvas.transform.localScale;
    }

    void Update()
    {
        if (_alarmStopped)
            return;

        _noiseCanvas.transform.LookAt(_mainCameraTransform);

        float animateFactor = Mathf.Sin(Time.time * 3f);

        _noiseCanvas.transform.localScale = _initialNoiseCanvasScale + new Vector3(animateFactor, animateFactor, animateFactor) / 2.3f;

        if(Time.time > _alarmEnd)
        {
            StopAlarm();

            CancelInvoke(nameof(ActivateLight));
            CancelInvoke(nameof(DeactivateLight));

            _noiseCanvas.SetActive(false);

            _alarmSource.Stop();
            _glassBreakSource.Stop();

            DeactivateLight();
        }
    }

    public void StartAlarm()
    {
        _alarmEnd = Time.time + _alarmDuration;

        InvokeRepeating(nameof(ActivateLight), 0f, 1.4f);
        InvokeRepeating(nameof(DeactivateLight), .7f, 1.4f);

        _noiseCanvas.SetActive(true);

        _alarmSource.Play();
        _glassBreakSource.Play();
    }

    private void ActivateLight()
    {
        _lightParent.SetActive(true);
    }

    private void DeactivateLight()
    {
        _lightParent.SetActive(false);
    }

    private void StopAlarm()
    {
        _alarmStopped = true;
    }
}

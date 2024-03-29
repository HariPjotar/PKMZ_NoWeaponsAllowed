using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class GuardLookAroundScript : MonoBehaviour
{
    [SerializeField] private GuardWanderingManager _manager;

    [SerializeField, Tooltip("How often the guard should look around(In seconds).")] private float _lookAroundFrequency;
    [SerializeField, Tooltip("How long it takes the guard to look around(In seconds).")] private float _lookAroundTime; 
    private float _nextLookTime;

    [SerializeField] private GameObject _guardModel;

    void Start()
    {
        if (_manager == null)
            _manager = GetComponent<GuardWanderingManager>();

        _nextLookTime = Time.time + _lookAroundFrequency;

        GetComponent<GuardWanderingManager>().OnKnockOut += GuardLookAroundScript_OnKnockOut;
    }

    void Update()
    {

        if (_manager.CurrentGuardState == GuardStates.KNOCKED_OUT || _manager.CurrentGuardState == GuardStates.AGGROED)
        {
            _nextLookTime = Time.time + _lookAroundTime + _lookAroundFrequency;

            return;
        }

        ManageLookAroundTimer();
    }

    private void ManageLookAroundTimer()
    {
        if(Time.time > _nextLookTime)
        {
            LookAround();

            _nextLookTime = Time.time + _lookAroundTime + _lookAroundFrequency;
        }
    }

    private void LookAround()
    {
        Tween.Rotate(_guardModel.transform, new Vector3(0f, 0f, 45f), Space.Self, (_lookAroundTime / 3f) * .9f, 0f, Tween.EaseInOutStrong);
        Tween.Rotate(_guardModel.transform, new Vector3(0f, 0f, -90f), Space.Self, (_lookAroundTime / 3f) * .9f, (_lookAroundTime / 3f), Tween.EaseInOutStrong);
        Tween.Rotate(_guardModel.transform, new Vector3(0f, 0f,  45f), Space.Self, (_lookAroundTime / 3f) * .9f, (_lookAroundTime / 3f) * 2f, Tween.EaseInOutStrong);
    }

    private void GuardLookAroundScript_OnKnockOut()
    {
        PlayKnockPutAnimation();
    }

    private void PlayKnockPutAnimation()
    {
        Tween.Rotate(_guardModel.transform, new Vector3(0f, -20f, 0f), Space.Self, .3f, 0f, Tween.EaseWobble);
        Tween.Rotate(_guardModel.transform, new Vector3(-90f, 0f, 0f), Space.Self, .2f, .4f, Tween.EaseBounce);
    }
}

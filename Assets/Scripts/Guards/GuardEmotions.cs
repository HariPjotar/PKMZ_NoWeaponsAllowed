using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;

public class GuardEmotions : MonoBehaviour
{
    [SerializeField] private GameObject _thoughtBubbleCanvas;

    [SerializeField] private GuardWanderingManager _guardManager;

    //Hard code: 0 - ... , 1 - ?, 2 - !!!
    [SerializeField] private List<Sprite> _guardEmotions;

    [SerializeField] private Image _emotionImage;

    [SerializeField] private float _emotionCooldown;
    [SerializeField] private float _emotionDuration;

    private float _emotionTimer;
    private float _nextEmotionTime;
    private float _hideEmotionTime;

    private bool _showedAggroEmotion;

    void Start()
    {
        _thoughtBubbleCanvas.transform.localScale = Vector3.zero;

        _emotionImage = _thoughtBubbleCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();

        _nextEmotionTime = Time.time + Random.Range(_emotionDuration * 1.2f, _emotionCooldown * 3f);

        _guardManager.OnKnockOut += _guardManager_OnKnockOut;
    }

    private void _guardManager_OnKnockOut()
    {
        HideThoughtBubble();
    }

    private void OnDestroy()
    {
        _guardManager.OnKnockOut -= _guardManager_OnKnockOut;
    }

    void Update()
    {
        if (_guardManager.CurrentGuardState == GuardStates.KNOCKED_OUT)
            return;

        if(_guardManager.CurrentGuardState == GuardStates.WANDERING || _guardManager.CurrentGuardState == GuardStates.WAITING)
        {
            if (_showedAggroEmotion)
            {
                HideThoughtBubble();
                _nextEmotionTime = Time.time + .5f;
                _showedAggroEmotion = false;
            }

            _emotionTimer += Time.deltaTime;

            if (Time.time > _hideEmotionTime)
            {
                HideThoughtBubble();

                _hideEmotionTime = Time.time + 100f;
            }

            if (_emotionTimer > _nextEmotionTime)
            {
                _nextEmotionTime = Time.time + Random.Range(_emotionCooldown, _emotionCooldown * 3f);
                _hideEmotionTime = Time.time + _emotionDuration;

                ShowThoughtBubble(0);
            }
        }

        if(_guardManager.CurrentGuardState == GuardStates.INVESTIGATING)
        {
            if (_showedAggroEmotion)
            {
                HideThoughtBubble();
                _nextEmotionTime = Time.time + .5f;
                _showedAggroEmotion = false;
            }

            _emotionTimer += Time.deltaTime;

            if (Time.time > _hideEmotionTime)
            {
                HideThoughtBubble();

                _hideEmotionTime = Time.time + 100f;
            }

            if (_emotionTimer > _nextEmotionTime)
            {
                _nextEmotionTime = Time.time + _emotionDuration * 1.1f;
                _hideEmotionTime = Time.time + _emotionDuration;

                ShowThoughtBubble(1);
            }
        }

        if(_guardManager.CurrentGuardState == GuardStates.AGGROED)
        {
            if (!_showedAggroEmotion)
            {
                HideThoughtBubble();
                ShowThoughtBubble(2);
                _showedAggroEmotion = true;
            }
        }
    }

    private void ShowThoughtBubble(int emotion)
    {
        _emotionImage.sprite = _guardEmotions[emotion]; 

        Tween.LocalScale(_thoughtBubbleCanvas.transform, new Vector3(1f, 1f, 1f), .6f, 0f, Tween.EaseBounce);
    }

    private void HideThoughtBubble()
    {
        Tween.LocalScale(_thoughtBubbleCanvas.transform, new Vector3(0f, 0f, 0f), .35f, 0f, Tween.EaseIn);
    }

    private void TurnOff()
    {
        this.enabled = false;
    }
}

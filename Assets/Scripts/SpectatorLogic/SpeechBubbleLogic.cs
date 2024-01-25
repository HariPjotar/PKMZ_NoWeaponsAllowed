using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;

public class SpeechBubbleLogic : MonoBehaviour
{
    [SerializeField] private GameObject _thoughtBubbleCanvas;

    [SerializeField] private List<Sprite> _emotions;

    [SerializeField] private Image _emotionImage;

    [SerializeField] private float _emotionCooldown;
    [SerializeField] private float _emotionDuration;

    private float _emotionTimer;
    private float _nextEmotionTime;
    private float _hideEmotionTime;

    void Start()
    {
        _thoughtBubbleCanvas.transform.localScale = Vector3.zero;

        _emotionImage = _thoughtBubbleCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();

        _nextEmotionTime = Time.time + Random.Range(_emotionDuration * 1.2f, _emotionCooldown * 3f);
    }

    void Update()
    {
        _emotionTimer += Time.deltaTime;

        if(Time.time > _hideEmotionTime)
        {
            HideThoughtBubble();

            _hideEmotionTime = Time.time + 100f;
        }

        if(_emotionTimer > _nextEmotionTime)
        {
            _nextEmotionTime = Time.time + Random.Range(_emotionCooldown, _emotionCooldown * 3f);
            _hideEmotionTime = Time.time + _emotionDuration;

            ShowThoughtBubble();
        }
    }

    private void ShowThoughtBubble()
    {
        _emotionImage.sprite = _emotions[Random.Range(0, _emotions.Count)];

        Tween.LocalScale(_thoughtBubbleCanvas.transform, new Vector3(1f, 1f, 1f), .6f, 0f, Tween.EaseBounce);
    }

    private void HideThoughtBubble()
    {
        Tween.LocalScale(_thoughtBubbleCanvas.transform, new Vector3(0f, 0f, 0f), .35f, 0f, Tween.EaseIn);
    }
}

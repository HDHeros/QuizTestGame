using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class LetterButton : MonoBehaviour, IBounceable, IEaseInBounce
{
    [SerializeField] private Image _imageUI;
    [SerializeField] private SeleteableGameObject _selecteableGameObject;


    public UnityEvent ClickedOnCorrectLetter;
    [SerializeField] public bool IsCorrectLetter;
    public SeleteableGameObject SeleteableGameObject
    {
        get
        {
            return _selecteableGameObject;
        }
        set
        {
            _selecteableGameObject = value;
            SetSprite();
        }
    }


    private Button _button;
    private RectTransform _rectTransform;
    private RectTransform _imageUIRectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _imageUIRectTransform = _imageUI.GetComponent<RectTransform>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        DoBounce(_rectTransform);
    }

    public void OnClick()
    {
        if(IsCorrectLetter)
        {
            DoBounce(_rectTransform);
            ClickedOnCorrectLetter.Invoke();
        }
        else
        {
            DoEaselnBounce(_imageUIRectTransform);
        }
    }

    private void SetSprite()
    {
        _imageUI.sprite = _selecteableGameObject.Sprite;
    }

    public void DoBounce(RectTransform animatedTransform)
    {
        var bounceSequence = DOTween.Sequence();
        bounceSequence.Append(_rectTransform.DOScale(0, 0));
        bounceSequence.Append(_rectTransform.DOScale(1.2f, 0.1f));
        bounceSequence.Append(_rectTransform.DOScale(0.95f, 0.1f));
        bounceSequence.Append(_rectTransform.DOScale(1f, 0.1f));
    }

    public void DoEaselnBounce(RectTransform objectTransform)
    {
        var easelnBounceSequence = DOTween.Sequence();
        float time = 0.1f;
        float startPositionX = objectTransform.position.x;
        easelnBounceSequence.Append(objectTransform.DOAnchorPosX(startPositionX - 1f, time, false));
        easelnBounceSequence.Append(objectTransform.DOAnchorPosX(startPositionX + 3f, time, false));
        easelnBounceSequence.Append(objectTransform.DOAnchorPosX(startPositionX - 7f, time, false));
        easelnBounceSequence.Append(objectTransform.DOAnchorPosX(startPositionX + 9f, time, false));
        easelnBounceSequence.Append(objectTransform.DOAnchorPosX(startPositionX - 11f, time, false));
        easelnBounceSequence.Append(objectTransform.DOAnchorPosX(startPositionX +8f, time, false));
        easelnBounceSequence.Append(objectTransform.DOAnchorPosX(startPositionX, time, false));
    }
}





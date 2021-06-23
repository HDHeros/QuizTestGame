using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class BlackPanel : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private Button _restartButton;

    private CanvasGroup _canvasGroup;
    private Image _image;
    private RectTransform _rectTransform;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _levelController.SessionEnded.AddListener(OnSessionEnded);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnSessionEnded()
    {
        _image.raycastTarget = true;
        _canvasGroup.DOFade(0.3f, 0.5f);
    }

    private void OnRestartButtonClick()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_canvasGroup.DOFade(1f, 0.3f));
        sequence.Append(_rectTransform.DOScaleY(0.1f, 0.2f));
        sequence.Append(_rectTransform.DOScaleX(0f, 1f));
        sequence.Append(_canvasGroup.DOFade(0f, 0f));
        sequence.Append(_rectTransform.DOScale(1f, 0f));


        _image.raycastTarget = false;
        _levelController.SetFirstLevel();

    }
}

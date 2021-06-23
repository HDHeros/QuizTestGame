using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Row : MonoBehaviour
{
    [SerializeField] private int _panelNumber;
    [SerializeField] private LevelController _levelController;

    private Image _image;
    private LetterButton[] _childLetterButtons;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _childLetterButtons = gameObject.GetComponentsInChildren<LetterButton>();
        _levelController.LevelChanged.AddListener(OnLevelChanged);
        OnLevelChanged();
    }

    private void OnLevelChanged()
    {
        if(_levelController.GetCurrentLevel() >= _panelNumber)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        _image.color = Color.white;
        SetActiveButtons(true);
    }

    private void Hide()
    {
        _image.color = Color.clear;
        SetActiveButtons(false);
    }

    private void SetActiveButtons(bool value)
    {
        foreach (LetterButton button in _childLetterButtons)
            button.gameObject.SetActive(value);
    }

}

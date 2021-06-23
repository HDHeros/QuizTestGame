using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;

    private Image _image;
    private Button _button;

    private void Start()
    {
        _image = GetComponent<Image>();
        _levelController.SessionEnded.AddListener(OnSessionEnded);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Hide);

    }

    private void OnSessionEnded()
    {
        Show();
    }

    private void Show()
    {
        _image.color = Color.white;
        _image.raycastTarget = true;
    }

    private void Hide()
    {
        _image.color = Color.clear;
        _image.raycastTarget = false;
    }
}

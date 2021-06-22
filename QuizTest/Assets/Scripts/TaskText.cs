using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TaskText : MonoBehaviour
{
    private Text _text;
    private void Start()
    {
        _text = GetComponent<Text>();
        _text.DOFade(0, 0);
        _text.DOFade(1f, 1f);

    }
}

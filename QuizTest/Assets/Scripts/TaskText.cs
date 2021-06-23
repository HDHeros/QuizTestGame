using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TaskText : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    private Text _text;
    private void Awake()
    {
        _text = GetComponent<Text>();
        _levelGenerator.CorrectButtonChanged.AddListener(OnCorrectButtonChanged);
    }

    public void OnCorrectButtonChanged(LetterButton newCorrectObject)
    {
         _text.text = "Find " + newCorrectObject.SeleteableGameObject.Name.ToUpper();
    }

    private void Fade()
    {
        _text = GetComponent<Text>();
        _text.DOFade(0, 0);
        _text.DOFade(1f, 1f);
    }
}

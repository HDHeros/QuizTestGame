using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour, ILevelController
{
    [SerializeField] private int _totalLevelCount;

    public UnityEvent SessionEnded;
    public UnityEvent LevelChanged;

    private int _currentLevel;
    private LevelGenerator _levelGenerator;



    private void Awake()
    {
        SetFirstLevel();
        SubscribeOnButtons();
        _levelGenerator = GetComponent<LevelGenerator>();
    }
    private void Start()
    {
        _levelGenerator.GenerateNewLevel();
    }
    public void SetNextLevel()
    {
        _currentLevel++;
        if (_currentLevel > _totalLevelCount)
        {
            SetFirstLevel();
            SessionEnded.Invoke();
        }
        LevelChanged.Invoke();
    }

    public void SetFirstLevel()
    {
        _currentLevel = 1;
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    private void OnCorrectButtonClick()
    {
        SetNextLevel();
        if (_currentLevel <= 3)
           _levelGenerator.GenerateNewLevel();
    }

    private void SubscribeOnButtons()
    {
        LetterButton[] buttons = GameObject.FindObjectsOfType<LetterButton>();
        foreach (LetterButton btn in buttons)
        {
            btn.ClickedOnCorrectLetter.AddListener(OnCorrectButtonClick);
        }
    }
}

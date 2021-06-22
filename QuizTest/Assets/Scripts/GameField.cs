using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class GameField : MonoBehaviour
{
    [SerializeField] private DataSet[] _dataSets;
    [SerializeField] private GameObject _firstRow;
    [SerializeField] private GameObject _secondRow;
    [SerializeField] private GameObject _thirdRow;
    [SerializeField] private Text _taskText;
    [SerializeField] private GameObject _restartButton;

    public UnityEvent SessionEnded;

    private List<SeleteableGameObject> _usedSelecteableGO; //объекты, которые уже были выбраны в качестве верных вариантов ответа в текущей сессии

    private int _levelNumber;
    private int LevelNumber
    {
        get
        {
            return _levelNumber;
        }
        set
        {
            _levelNumber = value;
            if(_levelNumber == 1)
            {
                _usedSelecteableGO.Clear();
            }
            if(_levelNumber == 4)
            {
                SessionEnded.Invoke();
                _restartButton.SetActive(true);

            }
        }
    }

    private void Start()
    {
        _usedSelecteableGO = new List<SeleteableGameObject>();
        _levelNumber = 1;
        SubscribeOnButtons();
        GenerateNewLevel();
    }

    private void GenerateNewLevel()
    {
        SetActiveForRows();
        SetSelecteableGOForButtons();
    }

    private void SubscribeOnButtons()
    {
        LetterButton[] buttons = GameField.FindObjectsOfType<LetterButton>();
        foreach(LetterButton btn in buttons)
        {
            btn.ClickedOnCorrectLetter.AddListener(OnCorrectButtonClick);
        }
        _restartButton.GetComponent<Button>().onClick.AddListener(OnRestartButtonClick);
    }

    private void SetActiveForRows()
    {
        _firstRow.SetActive(_levelNumber >= 1);
        _secondRow.SetActive(_levelNumber >= 2);
        _thirdRow.SetActive(_levelNumber >= 3);
    }

    private void SetSelecteableGOForButtons()
    {
        LetterButton[] activeButtons = GameObject.FindObjectsOfType<LetterButton>();
        List<SeleteableGameObject> dataSet = GetRandomDataSet();

        for(int i = 0; i < activeButtons.Length; i++)
        {
            int randomValue = UnityEngine.Random.Range(0, dataSet.Count - 1);

            SeleteableGameObject randomGO = dataSet[randomValue];
            activeButtons[i].SeleteableGameObject = randomGO;
            activeButtons[i].IsCorrectLetter = false;
            dataSet.Remove(randomGO);
        }

        int correctButtonIndex = UnityEngine.Random.Range(0, activeButtons.Length - 1);
        activeButtons[correctButtonIndex].IsCorrectLetter = true;
        _usedSelecteableGO.Add(activeButtons[correctButtonIndex].SeleteableGameObject);
        _taskText.text = "Find " + activeButtons[correctButtonIndex].SeleteableGameObject.Name.ToUpper();
    }

    private List<SeleteableGameObject> GetRandomDataSet()
    {
        int indexOfSet = UnityEngine.Random.Range(0, _dataSets.Length);
        IEnumerable<SeleteableGameObject> randomDataSet = new List<SeleteableGameObject>(_dataSets[indexOfSet].GetDataSet());
        IEnumerable<SeleteableGameObject> dataSetWithoutUsed = randomDataSet.Except(_usedSelecteableGO);
        return new List<SeleteableGameObject>(dataSetWithoutUsed);
    }

    public void OnRestartButtonClick()
    {
        _restartButton.SetActive(false);
        LevelNumber = 1;
        GenerateNewLevel();
    }

    private void OnCorrectButtonClick()
    {
        LevelNumber++;
        if (LevelNumber > 3)
        {
            _restartButton.SetActive(true);
        }
        else
        {
            GenerateNewLevel();
        }
    }
}

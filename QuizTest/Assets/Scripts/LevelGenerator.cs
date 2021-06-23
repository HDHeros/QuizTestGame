using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class EventReturningLB : UnityEvent<LetterButton> { }

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private DataSet[] _dataSets;

    public EventReturningLB CorrectButtonChanged;

    private LevelController _levelController;
    private List<SeleteableGameObject> _usedSelecteableGO;

    private void Start()
    {
        _usedSelecteableGO = new List<SeleteableGameObject>();
        _levelController = GetComponent<LevelController>();
        _levelController.SessionEnded.AddListener(OnSessionEnded);

    }

    public void GenerateNewLevel()
    {
        SetSelecteableGOForButtons();
    }

    private void SetSelecteableGOForButtons()
    {
        LetterButton[] activeButtons = GameObject.FindObjectsOfType<LetterButton>();
        List<SeleteableGameObject> dataSet = GetRandomDataSet();

        foreach(LetterButton currentButton in activeButtons)
        { 
            SetRandomSeleteableGO(dataSet, currentButton);
        }

        SetRandomButtonCorrect(activeButtons);

    }

    private void SetRandomSeleteableGO(List<SeleteableGameObject> dataSet, LetterButton letterButton)
    {
        int randomValue = UnityEngine.Random.Range(0, dataSet.Count - 1);
        SeleteableGameObject randomGO = dataSet[randomValue];
        letterButton.SeleteableGameObject = randomGO;
        letterButton.IsCorrectLetter = false;
        dataSet.Remove(randomGO);
    }

    private void SetRandomButtonCorrect(LetterButton[] activeButtons)
    {
        int correctButtonIndex = UnityEngine.Random.Range(0, activeButtons.Length - 1);
        LetterButton choosenButton = activeButtons[correctButtonIndex];
        choosenButton.IsCorrectLetter = true;
        _usedSelecteableGO.Add(choosenButton.SeleteableGameObject);
        CorrectButtonChanged.Invoke(choosenButton);
    }

    private List<SeleteableGameObject> GetRandomDataSet()
    {
        int indexOfSet = UnityEngine.Random.Range(0, _dataSets.Length);
        IEnumerable<SeleteableGameObject> randomDataSet = new List<SeleteableGameObject>(_dataSets[indexOfSet].GetDataSet());
        IEnumerable<SeleteableGameObject> dataSetWithoutUsed = randomDataSet.Except(_usedSelecteableGO);
        return new List<SeleteableGameObject>(dataSetWithoutUsed);
    }

    private void OnSessionEnded()
    {
        _usedSelecteableGO.Clear();
    }
}

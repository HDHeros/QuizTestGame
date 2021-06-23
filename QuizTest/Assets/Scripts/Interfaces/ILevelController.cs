using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILevelController
{ 
    void SetNextLevel();
    void SetFirstLevel();
    int GetCurrentLevel();
}

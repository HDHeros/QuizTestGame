using UnityEngine;

[System.Serializable]
public class SeleteableGameObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;

    public Sprite Sprite
    {
        get
        {
            return _sprite;
        }
    }
    public string Name
    {
        get
        {
            return _name;
        }
    }
}

using UnityEngine;

public static class Tag
{
    public static bool PossedeTag(string _tag, GameObject _obj)
    {
        // extraction des tags de l'objet
        string[] _listTag = _obj.tag.Split(',');

        foreach (string item in _listTag)
        {
            if (item == _tag)
                return true;
        }

        return false;
    }
}

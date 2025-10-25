using UnityEngine;

[System.Serializable]
public struct LimbData
{
    public GameObject prefab; // the prefab to instantiate
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;
}

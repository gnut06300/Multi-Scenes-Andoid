using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/data")]
public class Data : ScriptableObject
{
    [SerializeField]
    public Vector3 position;
    public int sceneIndex;
    public float soundVolume = 0.5f;
}

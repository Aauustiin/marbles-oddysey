using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog Data")]
public class Dialog : ScriptableObject
{
    [SerializeField] public string text;
}

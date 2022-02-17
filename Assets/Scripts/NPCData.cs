using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "NPC Data")]
public class NPCData : ScriptableObject
{
    [SerializeField] public string dialog;
}

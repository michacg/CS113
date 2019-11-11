using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Completion", menuName = "Completion")]
public class Completion : ScriptableObject
{
    public MinorTask task;
    public int NumberOfActions;
    public CompletionType type;
}

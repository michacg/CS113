using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Completion", menuName = "Completion")]
public class Completion : ScriptableObject
{
    public MinorTask task;
    public int actionsToPerform;
    public int NumberOfActions;
    public CompletionType type;

    private void OnEnable()
    {
        NumberOfActions = actionsToPerform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Completion", menuName = "Completion")]
public class Completion : ScriptableObject
{
    public List<MinorTask> tasks;
    public int actionsToPerform;
    public int NumberOfActions;
    public CompletionType type;
    public bool shouldBeDroppedOnCompletion = false;
    public bool shouldDestroyOnCompletion = false;

    private void OnEnable()
    {
        NumberOfActions = actionsToPerform;
    }
}

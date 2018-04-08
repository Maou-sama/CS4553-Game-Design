using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="PluggableAI/State")]
public class State : ScriptableObject {

    public Action[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColor = Color.gray;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransition(controller);
    }

    public void DoActions(StateController controller)
    {
        for(int i=0; i<actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransition(StateController controller)
    {
        for(int i=0; i<transitions.Length; i++)
        {
            bool desicionSucceded = transitions[i].desicion.Decide(controller);

            if(desicionSucceded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}

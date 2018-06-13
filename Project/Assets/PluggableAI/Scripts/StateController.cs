using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

    public State current_state;
	public Transform eyes;
    public State remainState;



	[HideInInspector] public NavMeshAgent navMeshAgent;
    public List<Transform> wayPointList;
    [HideInInspector] public int next_point;
     public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapse;

	private bool aiActive;


	void Awake () 
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
	{
		wayPointList = wayPointsFromTankManager;
		aiActive = aiActivationFromTankManager;
		if (aiActive) 
		{
			navMeshAgent.enabled = true;
		} else 
		{
			navMeshAgent.enabled = false;
		}
	}

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            current_state = nextState;
            OnExitState();
        }
    }

    public void OnExitState()
    {
        stateTimeElapse = 0;
    }

    public bool CheckIfCountDownElapse(float duration)
    {
        stateTimeElapse += Time.deltaTime;
        return stateTimeElapse >= duration;
    }

    private void Update()
    {
        if (!aiActive)
            return;
        current_state.UpdateState(this);
        
    }

    private void OnDrawGizmos()
    {
        if(current_state != null && eyes != null)
        {
            Gizmos.color = current_state.sceneGizmoColor;
        }
    }

}
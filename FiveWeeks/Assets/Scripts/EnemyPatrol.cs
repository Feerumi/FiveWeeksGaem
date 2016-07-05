using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyPatrol : EnemyBehaviour.Patrol {

	[SerializeField] private Transform[] path;
	private NavMeshAgent agent;
	private int mCurrentDestination = 0;
	private float goalRadius = 0.5f;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();

		agent.autoBraking = false;
		gotoNextPoint ();
	}
	
	// Update is called once per frame
	void Update () {
		if (agent.remainingDistance <= goalRadius)
			gotoNextPoint ();
	}

	public override void onPatrolStateChanged (EnemyBehaviour.PatrolState newState)
	{
		// Do stuff. Like go back to orginal patrol path.
	}

	protected void gotoNextPoint() {
		if (path.Length != 0) {
			mCurrentDestination = (mCurrentDestination + 1) % path.Length;
			agent.destination = path [mCurrentDestination].position;
		}
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (EnemyBehaviour))]
public class EnemyPatrol : EnemyBehaviour.Patrol {

	// Series of points which the enemy will follow.
	[SerializeField] private Transform[] path;

	// How close can the enemy get to a way point, before starting to move towards the next one.
	[SerializeField][Range (0, Mathf.Infinity)] private float goalRadius;
	private NavMeshAgent agent;

	// Index of the waypoint that is currently active.
	private int mCurrentDestination = 0;
	private EnemyBehaviour behaviour;

	void Awake() {
		agent = GetComponent<NavMeshAgent> ();
		behaviour = GetComponent<EnemyBehaviour> ();
	}

	void Start() {
		agent.autoBraking = false;
		nextPatrolPoint ();
	}
	
	// Update is called once per frame
	void Update () {
		switch(behaviour.EnemyPatrolState) {
		case EnemyBehaviour.PatrolState.CHASE:
			agent.destination = behaviour.vision.playerLastSeen ();
			break;

		case EnemyBehaviour.PatrolState.SEARCH:
			break;

		default:
			if (agent.remainingDistance <= goalRadius)
				nextPatrolPoint ();
			break;
		}
	}

	public override void onPatrolStateChanged (EnemyBehaviour.PatrolState newState) {
		// Do stuff. Like go back to orginal patrol path or chase the player.
	}

	protected void nextPatrolPoint() {
		if (path.Length != 0) {
			mCurrentDestination = (mCurrentDestination + 1) % path.Length;
			agent.destination = path [mCurrentDestination].position;
		}
	}
}

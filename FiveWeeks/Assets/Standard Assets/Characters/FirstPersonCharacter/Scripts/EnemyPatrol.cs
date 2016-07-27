using UnityEngine;
using System.Collections;

[RequireComponent (typeof (EnemyBehaviour))]
public class EnemyPatrol : EnemyBehaviour.Patrol {

	// Series of points which the enemy will follow.
	private Transform[] mPath;
	[SerializeField] private GameObject path;

	// How close can the enemy get to a way point, before starting to move towards the next one.
	[SerializeField][Range (0, 1000)] private float goalRadius;

	// Index of the waypoint that is currently active.
	private int mCurrentDestination = 0;
	private EnemyBehaviour behaviour;

	void Awake() {
		behaviour = GetComponent<EnemyBehaviour> ();
		if (path != null)
			mPath = path.GetComponentsInChildren<Transform> ();
	}

	void Start() {
		behaviour.agent.autoBraking = false;
		nextPatrolPoint ();
	}
	
	// Update is called once per frame
	void Update () {
		switch(behaviour.EnemyPatrolState) {
		case EnemyBehaviour.PatrolState.CHASE:
            behaviour.agent.destination = behaviour.pointOfIntrest;
			break;

		case EnemyBehaviour.PatrolState.INVESTIGATE:
			break;

		default:
            if (behaviour.agent.remainingDistance <= goalRadius)
				nextPatrolPoint ();
			break;
		}
	}

	public override void onPatrolStateChanged (EnemyBehaviour.PatrolState newState) {
		// Do stuff. Like go back to orginal patrol path or chase the player.
	}

	protected void nextPatrolPoint() {
		if (mPath != null && mPath.Length != 0) {
			mCurrentDestination = (mCurrentDestination + 1) % mPath.Length;
            behaviour.agent.destination = mPath [mCurrentDestination].position;
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine;


public class EnemyBehaviour : MonoBehaviour {

	[SerializeField] private Vision mVision;
	[SerializeField] private Patrol mPatrol;

	// Current state of awareness.
	private PatrolState mState {
		get{
			return mState;
		} 
		set {
			mState = value;
			mPatrol.onPatrolStateChanged(mState);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public enum PatrolState {
		CHASE, PATROL_LOW_ALERT, PATROL_MEDIUM_ALERT, PATROL_HIGH_ALERT, SEARCH
	};

	public interface Vision {
		bool canSeePlayer ();
		void onPlayerSeen ();
		Vector3 playerLastSeen ();
	}

	public abstract class Patrol : MonoBehaviour {
		public abstract void onPatrolStateChanged(PatrolState newState);
	}
}

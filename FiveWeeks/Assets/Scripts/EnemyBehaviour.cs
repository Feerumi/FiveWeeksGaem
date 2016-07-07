using UnityEngine;
using System.Collections;


public class EnemyBehaviour : MonoBehaviour, VisibilityListener {

	// Reference to script that handles seeing the player.
	public Vision vision { get; set; }
	// Reference to the script that handles navigatin the 
	public Patrol patrol { get; set; }
	private PatrolState mPatrolState;
	private bool hasSeenPlayerBefore;

	// Current state of awareness.
	public PatrolState EnemyPatrolState {
		get {
			return mPatrolState;
		}

		set {
			if (value != mPatrolState) {
				mPatrolState = value;
				if (patrol != null)
					patrol.onPatrolStateChanged(mPatrolState);
			}
		}
	}

	void Awake() {
		vision = GetComponent<Vision> ();
		if (vision != null)
			vision.setVisibilityListener (this);
		patrol = GetComponent<Patrol> ();
	}

	// Use this for initialization
	void Start () {
		// TODO Expose to inspector?
		mPatrolState = PatrolState.PATROL_LOW_ALERT;
		hasSeenPlayerBefore = false;
	}

	void VisibilityListener.onPlayerSeen() {
		EnemyPatrolState = PatrolState.CHASE;
	}

	void VisibilityListener.onPlayerHide() {
		returnToPatrol ();
	}

	void  returnToPatrol() {
		EnemyPatrolState = (hasSeenPlayerBefore) ? PatrolState.PATROL_HIGH_ALERT : PatrolState.PATROL_MEDIUM_ALERT;
		hasSeenPlayerBefore = true;	
	}

	public enum PatrolState {
		CHASE, PATROL_LOW_ALERT, PATROL_MEDIUM_ALERT, PATROL_HIGH_ALERT, SEARCH
	};

	public abstract class Vision : MonoBehaviour {

		private VisibilityListener listener;
		private bool mPlayerInSight = false;

		public bool PlayerInSight {
			get {
				return mPlayerInSight;
			}

			set {
				if (value != mPlayerInSight) {
						mPlayerInSight = value;
					if (listener != null) {
						if (value) {
							listener.onPlayerSeen ();
						} else {
							listener.onPlayerHide ();
						}
					}
				}
			}
		}
			
		public abstract Vector3 playerLastSeen ();

		public void setVisibilityListener(VisibilityListener listener) {
			this.listener = listener;
		}
	}

	public abstract class Patrol : MonoBehaviour {
		public abstract void onPatrolStateChanged(PatrolState newState);
	}
}

using UnityEngine;
using System.Collections;


[RequireComponent (typeof (NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour, VisibilityListener, SoundListener {

	// Reference to script that handles seeing the player.
	public Vision vision { get; set; }
	// Reference to the script that handles navigatin the map.
	public Patrol patrol { get; set; }
	// Reference to the script that handles hearing related functions.
	public Hearing hearing { get; set; }
	// Last known point of intrest. Can be a source of sound, player sighting etc.
	public Vector3 pointOfIntrest { get; set; }

	private PatrolState mPatrolState;
	private bool hasSeenPlayerBefore;
    public NavMeshAgent agent{ get; set; }

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
		hearing = GetComponentInChildren<Hearing> ();
		if (hearing != null)
			hearing.setSoundListener (this);
		patrol = GetComponent<Patrol> ();
        agent = GetComponent<NavMeshAgent> ();
	}

	// Use this for initialization
	void Start () {
		// TODO Expose to inspector?
		mPatrolState = PatrolState.PATROL_LOW_ALERT;
		hasSeenPlayerBefore = false;
	}

	#region VisibilityListener implementation

	void VisibilityListener.onPlayerSeen() {
		EnemyPatrolState = PatrolState.CHASE;
	}

	void VisibilityListener.onPlayerHide() {
		returnToPatrol ();
	}

	#endregion

	#region SoundListener implementation

	public void onObjectAudible ()
	{
		switch (EnemyPatrolState) {
		case PatrolState.CHASE:
			break;

		default:
			EnemyPatrolState = PatrolState.INVESTIGATE;
			break;
		}
	}

	public void onObjectInaudible ()
	{
		// TODO React to a sound becoming inaudiable.
		// Doesn't necessarily need an implementation.
	}

	#endregion

	void  returnToPatrol() {
		EnemyPatrolState = (hasSeenPlayerBefore) ? PatrolState.PATROL_HIGH_ALERT : PatrolState.PATROL_MEDIUM_ALERT;
		hasSeenPlayerBefore = true;	
	}

	public enum PatrolState {
		CHASE, PATROL_LOW_ALERT, PATROL_MEDIUM_ALERT, PATROL_HIGH_ALERT, INVESTIGATE
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

    public abstract class Hearing : MonoBehaviour {
		private SoundListener listener;
		private bool mObjectHeard = false;

		public bool ObjectHeard {
			get {
				return mObjectHeard;
			}

			set {
				if (value != mObjectHeard) {
					mObjectHeard = value;
					if (listener != null) {
						if (value) {
							listener.onObjectAudible ();
						} else {
							listener.onObjectInaudible();
						}
					}
				}
			}
		}

		public void setSoundListener(SoundListener listener) {
			this.listener = listener;
		}
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (EnemyBehaviour))]
[RequireComponent (typeof (SphereCollider))]
public class EnemyVision : EnemyBehaviour.Vision {

	[SerializeField][Range(0, 360)]private float fieldOfView;
	private GameObject player;
	private PlayerVisibility playerVisibility;
	private EnemyBehaviour behaviour;
	private Vector3 playerLastSighting;
	private SphereCollider col;

	void Awake() {
		behaviour = GetComponent<EnemyBehaviour> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		if  (player != null) {
			playerVisibility = player.GetComponent<PlayerVisibility> ();
		}
		col = GetComponent<SphereCollider> ();
	}

	void Start() {
		col.isTrigger = true;
	}

	public override Vector3 playerLastSeen() {
		return playerLastSighting;
	}

	void OnTriggerStay(Collider other) {
		
		if (player != null && other.gameObject.tag == player.gameObject.tag) {
			bool inSight = false;

			if (playerVisibility.isVisible()) {
				Vector3 direction = other.transform.position - this.transform.position;
				float angle = Vector3.Angle (direction, this.transform.forward);

				if (angle <= fieldOfView / 2) {
					RaycastHit hit;
					// Raycast originates from the x,y,z coordinate of the enemy. Might need adjustment based on
					// where this point resides within the enemy model.
					if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius)) {
						if (hit.collider.gameObject == player) {
							playerLastSighting = player.gameObject.transform.position;
							inSight = true;
						}
					}
				}
			}

			PlayerInSight = inSight;
		}
	}

	void OnTriggerExit(Collider other) {
		if (player != null && other.gameObject == player) {
			PlayerInSight = false;
		}
	}
}

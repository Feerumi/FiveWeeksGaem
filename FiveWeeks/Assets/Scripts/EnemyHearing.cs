using UnityEngine;
using System.Collections;
using System;
using UnityEditor;


[RequireComponent (typeof (SphereCollider))]
public class EnemyHearing : EnemyBehaviour.Hearing {

    [Serializable]
    public class HearingSettings {
        public float m_HearingRange = 100f;
    }

    [SerializeField] private HearingSettings hearSettings = new HearingSettings();
    GameObject mPlayer;
    float mDistanceToAudioSource;
    private SphereCollider col;
    private EnemyBehaviour mBehaviour;
   

    void Awake() {
        mPlayer = GameObject.FindGameObjectWithTag ("Player");
        col = GetComponent<SphereCollider> ();
        mBehaviour = GetComponentInParent<EnemyBehaviour> ();
    }

    public void SoundHeard (Vector3 pos, float loudness) {
        RaycastHit hit;
        Vector3 direction = pos - this.transform.position;
        // Raycast originates from the x,y,z coordinate of the enemy. Might need adjustment based on
        // where this point resides within the enemy model.
        if (Physics.Raycast (transform.position, direction.normalized, out hit, col.radius)) {
            if (hit.collider.gameObject == mPlayer) {
                mDistanceToAudioSource = hit.distance;
            } else {
                mDistanceToAudioSource = CalculatePathLength (pos);
            }
        }
			
		// Is the sound within hearing range.
		if (mDistanceToAudioSource <= col.radius) {
			// TODO check sound loudness.
			ObjectHeard = true;
		} else {
			ObjectHeard = false;
		}
    }

    float CalculatePathLength(Vector3 targetPosition) {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if(mBehaviour.agent.enabled)
            mBehaviour.agent.CalculatePath(targetPosition, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        // The points inbetween are the corners of the path.
        for(int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for(int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeSound : MonoBehaviour {

    private List<EnemyHearing> mEnemies;

	// Use this for initialization
	void Start () {
        mEnemies = new List<EnemyHearing> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
    public void SoundPlayed(float soundLoudness) {
        foreach (EnemyHearing enemy in mEnemies) {
            enemy.SoundHeard (gameObject.transform.position, soundLoudness);
        }
    }

    void OnTriggerEnter(Collider coll) {
        EnemyHearing tmp = coll.gameObject.GetComponent<EnemyHearing> ();

        if (tmp != null) {
            mEnemies.Add (tmp);
        } else {
            tmp = coll.gameObject.GetComponentInChildren<EnemyHearing> ();

            if (tmp != null) {
                mEnemies.Add (tmp);
            } else {
                tmp = coll.gameObject.GetComponentInParent<EnemyHearing> ();

                if (tmp != null) {
                    mEnemies.Add (tmp);
                }
            }
        }
    }

    void OnTriggerExit(Collider coll) {
        EnemyHearing tmp = coll.gameObject.GetComponent<EnemyHearing> ();

        if (tmp != null) {
            mEnemies.Remove (tmp);
        } else {
            tmp = coll.gameObject.GetComponentInChildren<EnemyHearing> ();

            if (tmp != null) {
                mEnemies.Remove (tmp);
            } else {
                tmp = coll.gameObject.GetComponentInParent<EnemyHearing> ();

                if (tmp != null) {
                    mEnemies.Remove (tmp);
                }
            }
        }
    }
}

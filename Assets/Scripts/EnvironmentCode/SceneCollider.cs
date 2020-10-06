using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SceneDialogue))]
[RequireComponent(typeof(BoxCollider))]
public class SceneCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {

        if (other.GetComponent<Player>()) {
            GetComponent<SceneDialogue>().enabled = true;
        }
    }
}

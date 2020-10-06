using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class DialogueManager : MonoBehaviour {
    [SerializeField] private Text dialogue;
    [SerializeField] private Text whoseTalking;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private GameObject dialogueScreen;
    private bool dialogueIsRunning;
    public static event UnityAction requestNextLine;
    public static event UnityAction skipDialogue;

    void Start() {
        SceneDialogue.pullUpDialogue += DialogueUp;
        SceneDialogue.turnOffDialogue += DialogueUp;
        SceneDialogue.sendName += SetTalker;
        SceneDialogue.sendLine += SetDialogue;
    }

    void Update() {
        if (dialogueIsRunning) {
            if (Input.GetButtonDown("Fire2")) {
                if (requestNextLine != null) {
                    requestNextLine();
                }
            }
            if (Input.GetButtonDown("Fire1")) {
                if (skipDialogue != null) {
                    skipDialogue();
                }
                if (requestNextLine != null) {
                    requestNextLine();
                }
            }
        }
    }
    private void DialogueUp(bool val) {
        textPanel.SetActive(val);
        dialogueScreen.SetActive(val);
        dialogueIsRunning = val;
    }
    private void SetTalker(string name) {
        whoseTalking.text = name;
    }
    private void SetDialogue(string text) {
        dialogue.text = text;
    }
}

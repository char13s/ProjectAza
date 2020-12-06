using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputConsole : MonoBehaviour
{
    [SerializeField] private Text console;

    private void Start() {
        PlayerCommands.sendInput += AddText;
    }
    private void AddText(string val) {
        console.text = val;

    }
}

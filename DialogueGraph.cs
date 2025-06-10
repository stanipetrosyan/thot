namespace thot;

using UnityEngine;
using UnityEditor;

public class DialogueGraph: EditorWindow {

    [MenuItem("Plugins/Dialogue Graph")]
    public static void OpenDialogueGraphWindow() {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }
}
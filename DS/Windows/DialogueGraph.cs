using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Windows;

public class DialogueGraph: EditorWindow {

    [MenuItem("Plugins/Dialogue Graph")]
    public static void OpenDialogueGraphWindow() {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }
    
    public void OnEnable() {
        AddGraphView();
    }

    private void AddGraphView() {
        DialogueGraphView dialogueGraphView = new DialogueGraphView();
            
        dialogueGraphView.StretchToParentSize();
        rootVisualElement.Add(dialogueGraphView);
            
    }
}
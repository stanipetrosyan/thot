using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Windows;

public class DSEditorWindow : EditorWindow {
    //private DSGraphView graphView { get; set; }

    [MenuItem("Tests/Dialogue Graph")]
    public static void OpenDialogueGraphWindow() {
        var window = GetWindow<DSEditorWindow>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    public void OnEnable() {
        AddGraphView();
    }

    private void AddGraphView() {
        DSGraphView dsGraphView = new DSGraphView(this);

        dsGraphView.StretchToParentSize();
        rootVisualElement.Add(dsGraphView);

        //this.graphView = dsGraphView;
    }
}
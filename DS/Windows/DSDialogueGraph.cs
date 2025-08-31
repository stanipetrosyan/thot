using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Windows {
    public class DSDialogueGraph: EditorWindow {

        [MenuItem("Plugins/Dialogue Graph")]
        public static void OpenDialogueGraphWindow() {
            var window = GetWindow<DSDialogueGraph>();
            window.titleContent = new GUIContent("Dialogue Graph");
        }
    
        public void OnEnable() {
            AddGraphView();
        }

        private void AddGraphView() {
            DSGraphView dsGraphView = new DSGraphView();
            
            dsGraphView.StretchToParentSize();
            rootVisualElement.Add(dsGraphView);
            
        }
    }
}
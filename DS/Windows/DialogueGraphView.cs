using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Windows;

public class DialogueGraphView: GraphView {
    
    public DialogueGraphView() {
        AddGridBackground();
            
        AddStyles();
        AddManipulators();
    }
    
    private void AddManipulators() {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

    }
    
    private void AddGridBackground() {
        GridBackground gridBackground = new GridBackground();
        gridBackground.StretchToParentSize();
            
        Insert(0, gridBackground);
    }
        
    private void AddStyles() {
        StyleSheet styleSheet = Resources.Load("Assets/Scripts/DS/Style/DSGraphViewStyle.uss") as StyleSheet;
        
        Debug.Log("ciao");
        styleSheets.Add(styleSheet);
    }
}
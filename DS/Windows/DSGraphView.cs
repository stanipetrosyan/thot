using thot.DS.Elements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace thot.DS.Windows;

public class DSGraphView : GraphView {
    public DSGraphView() {
        AddGridBackground();

        //AddStyles();
        AddManipulators();
    }

    private void AddManipulators() {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        this.AddManipulator(CreateContextualMenu(
            title: "Add Single Choice Node",
            action => AddElement(CreateNode("DialogueName", DSDialogueType.SINGLE
            ))
        ));
    }

    private IManipulator CreateContextualMenu(string title, Action<DropdownMenuAction> callback) {
        throw new NotImplementedException();
    }

    private DSNode CreateNode(string nodeName, DSDialogueType dialogueType) {
        DSNode node = dialogueType switch {
            DSDialogueType.SINGLE => new DSSingleChoiceNode(),
            DSDialogueType.MULTIPLE => new DSMultipleChoiceNode(),
            _ => throw new ArgumentOutOfRangeException(nameof(dialogueType), dialogueType, null)
        };
        
        AddElement(node);
        return node;
    }

    private void AddGridBackground() {
        GridBackground gridBackground = new GridBackground();
        gridBackground.StretchToParentSize();

        Insert(0, gridBackground);
    }

    /*private void AddStyles() {
        // TODO: Use embedded style
    }*/
}
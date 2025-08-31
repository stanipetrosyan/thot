using thot.DS.Domain;
using thot.DS.Elements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Windows {
    public class DSGraphView : GraphView {
        private DSSearchWindow searchWindow;
        private DSEditorWindow editorWindow;

        public DSGraphView(DSEditorWindow dsEditorWindow) {
            this.editorWindow = dsEditorWindow;

            AddGridBackground();

            //AddStyles();
            AddManipulators();
            AddSearchWindow();
        }

        private void AddManipulators() {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            this.AddManipulator(CreateContextualMenu(
                title: "Add Single Choice Node",
                action => CreateNode("DialogueName", DSDialogueType.Single, action.eventInfo.localMousePosition)
            ));

            this.AddManipulator(CreateContextualMenu(
                title: "Add Multiple Choice Node",
                action => CreateNode("DialogueName", DSDialogueType.Multiple, action.eventInfo.localMousePosition)
            ));

            this.AddManipulator(CreateContextualMenu(
                title: "Add Group",
                action => CreateGroup("DialogueName")
            ));
        }


        private IManipulator CreateContextualMenu(string title, Action<DropdownMenuAction> callback) {
            ContextualMenuManipulator contextualMenuManipulator =
                new ContextualMenuManipulator(@event => @event.menu.AppendAction(title, callback)
                );

            return contextualMenuManipulator;
        }

        private void AddSearchWindow() {
            if (searchWindow == null) {
                searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();
                searchWindow.Initialize(this);
            }

            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        public DSNode CreateNode(string nodeName, DSDialogueType dialogueType, Vector2 position) {
            DSNode node = dialogueType switch {
                DSDialogueType.Single => new DSSingleChoiceNode(),
                DSDialogueType.Multiple => new DSMultipleChoiceNode(),
                _ => throw new ArgumentOutOfRangeException(nameof(dialogueType), dialogueType, null)
            };

            node.Initialize(nodeName, position);

            AddElement(node);
            return node;
        }

        private void CreateGroup(string name) {
            DSGroup group = new DSGroup {
                title = name
            };

            AddElement(group);
        }

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false) {
            var worldMousePosition = mousePosition;

            if (isSearchWindow) {
                worldMousePosition -= editorWindow.position.position;
            }

            return contentViewContainer.WorldToLocal(worldMousePosition);
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
}
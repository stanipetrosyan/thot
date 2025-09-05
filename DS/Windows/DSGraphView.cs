using System;
using System.Collections.Generic;
using System.Linq;
using thot.DS.Domain;
using thot.DS.Elements;
using thot.DS.Style;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Windows {
    public class DSGraphView : GraphView {
        private DSSearchWindow _searchWindow;
        private readonly DSEditorWindow _editorWindow;
        /*private UngroupedNodes ungroupedNode;*/

        private Dictionary<string, DSNode> ungroupedNodes = new Dictionary<string, DSNode>();

        public DSGraphView(DSEditorWindow dsEditorWindow) {
            this._editorWindow = dsEditorWindow;

            AddGridBackground();

            AddManipulators();
            AddSearchWindow();

            OnGraphViewChanged();
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
            var contextualMenuManipulator =
                new ContextualMenuManipulator(@event => @event.menu.AppendAction(title, callback)
                );

            return contextualMenuManipulator;
        }

        private void AddSearchWindow() {
            if (_searchWindow == null) {
                _searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();
                _searchWindow.Initialize(this);
            }

            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
        }

        #region Graph View Elements Creation

        public DSNode CreateNode(string nodeName, DSDialogueType dialogueType, Vector2 position) {
            DSNode node;
            switch (dialogueType) {
                case DSDialogueType.Single:
                    node = new DSSingleChoiceNode();
                    break;
                case DSDialogueType.Multiple:
                    node = new DSMultipleChoiceNode();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dialogueType), dialogueType, null);
            }

            node.Initialize(nodeName, position);

            AddElement(node);
            AddUngroupedNode(node);
            return node;
        }

        private void AddUngroupedNode(DSNode node) {
            //TODO: duplicated node NAME case
            
            ungroupedNodes.Add(node.ID, node);
        }

        private void CreateEdges(List<Edge> edgesToCreate) {
            foreach (Edge edge in edgesToCreate) {
                DSNode nextNode = (DSNode)edge.input.node;
                DSChoice choiceData = (DSChoice)edge.output.userData;
                choiceData.NodeID = nextNode.ID;
            }
        }

        private void CreateGroup(string groupName) {
            DSGroup group = new DSGroup {
                title = groupName
            };

            AddElement(group);
        }

        #endregion

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false) {
            var worldMousePosition = mousePosition;

            if (isSearchWindow) {
                worldMousePosition -= _editorWindow.position.position;
            }

            return contentViewContainer.WorldToLocal(worldMousePosition);
        }


        #region Graph View Events

        private void OnGraphViewChanged() {
            graphViewChanged = (changes) => {
                if (changes.edgesToCreate != null) {
                    CreateEdges(changes.edgesToCreate);
                }

                return changes;
            };
        }

        #endregion

        #region Graph View Method Overrides

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
            List<Port> compatiblePorts = new List<Port>();

            ports.ForEach(port => {
                if (startPort == port || startPort.node == port.node || startPort.direction == port.direction) {
                    return;
                }

                compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        #endregion

        #region Graph View Style

        private void AddGridBackground() {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            gridBackground.style.backgroundColor = Colors.FromRGBToColor(43, 43, 43);

            Insert(0, gridBackground);
        }

        #endregion

        #region Graph Utility Methods

        public List<DSNode> GetNodes() {
            return ungroupedNodes.Values.ToList();
        }

        #endregion
    }
}
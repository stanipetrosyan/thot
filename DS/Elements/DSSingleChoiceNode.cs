using thot.DS.Domain;
using thot.DS.Windows;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace thot.DS.Elements {
    public class DSSingleChoiceNode : DSNode {
        public override void Initialize(string nodeName, Vector2 position) {
            base.Initialize(nodeName, position);

            DialogueType = DSDialogueType.Single;
            DSChoice choice = new DSChoice() {
                Text = "Next Dialogue"
            };

            Choices.Add(choice);
            Draw();
        }

        protected override void Draw() {
            base.Draw();

            foreach (var choice in Choices) {
                var port = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                port.portName = choice.Text;

                port.userData = choice;
                outputContainer.Add(port);
            }
        
            RefreshExpandedState();
        }
    }
}
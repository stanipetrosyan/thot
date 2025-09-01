using thot.DS.Domain;
using thot.DS.Style;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Elements {
    public class DSMultipleChoiceNode : DSNode {
        public override void Initialize(string nodeName, Vector2 position) {
            base.Initialize(nodeName, position);

            DialogueType = DSDialogueType.Multiple;

            var choice = new DSChoice() {
                Text = "Next Choice"
            };

            Choices.Add(choice);
        }

        protected override void Draw() {
            base.Draw();

            Button addChoiceButton = new DSButton("Add Choice", () => {
                DSChoice choice = new DSChoice {
                    Text = "Next Choice"
                };

                Choices.Add(choice);
            
                CreateChoice(choice);
            });
        
            mainContainer.Add(addChoiceButton);
            foreach (var choice in Choices) {
                CreateChoice(choice);
            }
        
        }

        private void CreateChoice(DSChoice choice) {
            Port choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            choicePort.portName = "";

            choicePort.userData = choice;

            TextField choiceTextField = new DSTextField("New Choice");
            choicePort.Add(choiceTextField);
            outputContainer.Add(choicePort);
        }
    }
}
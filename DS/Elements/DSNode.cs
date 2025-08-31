using thot.DS.Domain;
using thot.DS.Style;
using thot.DS.Windows;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Elements {
    public class DSNode : Node {
        public string ID { get; set; }
        public string DialogueName { get; set; }
        public string DialogueText { get; set; }
        public DSDialogueType DialogueType { get; set; }
        public List<DSChoice> Choices { get; set; }


        private TextField dialogueNameField;
        private TextField dialogueTextField;
        private VisualElement dialogueContainer;
        private Foldout foldout;


        public virtual void Initialize(string nodeName, Vector2 position) {
            ID = Guid.NewGuid().ToString();
            DialogueName = nodeName;
            DialogueText = "Dialogue Text";
            Choices = new List<DSChoice>();
            SetPosition(new Rect(position, Vector2.zero));
        }


        protected virtual void Draw() {
            dialogueNameField = new DSTextField(DialogueName, (callback) => DialogueName = callback.newValue);
            titleContainer.Add(dialogueNameField);
            Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            inputContainer.Add(input);

            dialogueContainer = new VisualElement();

            foldout = new Foldout {
                text = "Dialogue Text"
            };

            TextField dialogueText = new DSTextField("Dialogue Text", (callback) => DialogueText = callback.newValue) {
                multiline = true
            };
            foldout.Add(dialogueText);
            dialogueContainer.Add(foldout);
            extensionContainer.Add(dialogueContainer);

            SetStyle();
            RefreshExpandedState();
        }

        private void SetStyle() {
            mainContainer.style.backgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);

            /*dialogueContainer.style.marginLeft = new StyleLength(new Length(-4f));
            dialogueContainer.style.marginTop = new StyleLength(new Length(4f));*/
        
            foldout.style.marginLeft = new StyleLength(new Length(-4f));
            foldout.style.marginTop = new StyleLength(new Length(4f));
        }
    }
}
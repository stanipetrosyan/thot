using thot.DS.Domain;
using thot.DS.Style;
using thot.DS.Windows;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Elements;

public class DSNode : Node {
    public string ID { get; set; }
    public string DialogueName { get; set; }
    public string DialogueText { get; set; }
    public DSDialogueType DialogueType { get; set; }
    public List<DSChoice> Choices { get; set; }

    
    
    private TextField dialogueNameField;
    private TextField dialogueTextField;


    public virtual void Initialize(string nodeName, Vector2 position) {
        ID = Guid.NewGuid().ToString();
        DialogueName = nodeName;
        DialogueText = "Dialogue Text";
        SetPosition(new Rect(position, Vector2.zero));

        Draw();
        SetStyle();
    }


    protected virtual void Draw() {
        dialogueNameField = new DSTextField(DialogueName, (callback) => DialogueName = callback.newValue);

        titleContainer.Add(dialogueNameField);

        Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
        inputContainer.Add(input);

        VisualElement dataContainer = new VisualElement();

        Foldout foldout = new Foldout {
            text = "Dialogue Text"
        };

        TextField dialogueText = new DSTextField("Dialogue Text", (callback) => DialogueText = callback.newValue) {
            multiline = true
        };

        foldout.Add(dialogueText);
        dataContainer.Add(foldout);
        extensionContainer.Add(dataContainer);

        RefreshExpandedState();
    }

    private void SetStyle() {
        mainContainer.style.backgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);
    }
}
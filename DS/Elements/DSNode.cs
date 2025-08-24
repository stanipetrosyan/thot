using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace thot.DS.Elements;

public class DSNode : Node {
    public string ID { get; set; }
    public string DialogueName { get; set; }


    public void Initialize(string nodeName, Vector2 position) {
        ID = Guid.NewGuid().ToString();
        DialogueName = nodeName;
        SetPosition(new Rect(position, Vector2.zero));


        SetStyle();
        Draw();
    }

    private void SetStyle() {
        mainContainer.style.backgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);
    }

    private void Draw() {
        TextField dialogueNameField = new TextField {
            value = DialogueName
        };

        dialogueNameField.style.backgroundColor = GetColor(29, 29, 30);
        dialogueNameField.style.borderTopColor = GetColor(39, 41, 44);
        dialogueNameField.style.borderBottomColor = GetColor(39, 41, 44);
        ;
        dialogueNameField.style.borderLeftColor = GetColor(39, 41, 44);
        ;
        dialogueNameField.style.borderRightColor = GetColor(39, 41, 44);
        ;
        dialogueNameField.style.borderBottomLeftRadius = new StyleLength(new Length(4f));
        dialogueNameField.style.borderBottomRightRadius = new StyleLength(new Length(4f));
        dialogueNameField.style.borderTopRightRadius = new StyleLength(new Length(4f));
        dialogueNameField.style.borderTopLeftRadius = new StyleLength(new Length(4f));
        dialogueNameField.style.paddingBottom = new StyleLength(new Length(8f));
        dialogueNameField.style.paddingTop = new StyleLength(new Length(8f));
        dialogueNameField.style.paddingLeft = new StyleLength(new Length(8f));
        dialogueNameField.style.paddingRight = new StyleLength(new Length(8f));

        dialogueNameField.RegisterValueChangedCallback((callback) => { DialogueName = callback.newValue; });

        titleContainer.Add(dialogueNameField);

        Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
        inputContainer.Add(input);
    }


    private static Color GetColor(float red, float green, float blue, float alpha = 255) {
        return new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
    }
}
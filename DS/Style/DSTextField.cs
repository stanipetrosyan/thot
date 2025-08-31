using UnityEngine.UIElements;

namespace thot.DS.Style;

public class DSTextField : TextField {
    public DSTextField(string text, EventCallback<ChangeEvent<string>>? onChange = null) {
        //this.text = text;
        this.value = text;

        if (onChange != null) {
            this.RegisterValueChangedCallback(onChange);
        }
        this.
        
        style.backgroundColor = Colors.FromRGBToHex(29, 29, 30);
        style.borderTopColor = Colors.FromRGBToHex(39, 41, 44);
        style.borderBottomColor = Colors.FromRGBToHex(39, 41, 44);
        style.borderLeftColor = Colors.FromRGBToHex(39, 41, 44);
        style.borderRightColor = Colors.FromRGBToHex(39, 41, 44);
        style.borderBottomLeftRadius = new StyleLength(new Length(4f));
        style.borderBottomRightRadius = new StyleLength(new Length(4f));
        style.borderTopRightRadius = new StyleLength(new Length(4f));
        style.borderTopLeftRadius = new StyleLength(new Length(4f));
        style.paddingBottom = new StyleLength(new Length(8f));
        style.paddingTop = new StyleLength(new Length(8f));
        style.paddingLeft = new StyleLength(new Length(8f));
        style.paddingRight = new StyleLength(new Length(8f));
    }
}
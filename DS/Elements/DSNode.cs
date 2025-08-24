using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace thot.DS.Elements;

public class DSNode : Node {
    public string ID { get; set; }
    public string DialogueName { get; set; }


    public void Initialize(string nodeName, Vector2 position) {
        SetStyle();
        
        ID = Guid.NewGuid().ToString();
        DialogueName = nodeName;
        SetPosition(new Rect(position, Vector2.zero));
    }

    private void SetStyle() {
        mainContainer.style.backgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);
    }
}
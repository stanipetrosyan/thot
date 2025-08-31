using UnityEngine;

namespace thot.DS.Domain;

[Serializable]
public class DSChoice {
    [field: SerializeField] public string Text { get; set; }
    [field: SerializeField] public string NodeID { get; set; }
}
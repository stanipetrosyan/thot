using System;
using System.Collections.Generic;
using System.Linq;
using thot.DS.Domain;
using thot.DS.Domain.Save;
using thot.DS.Elements;
using thot.DS.Windows;
using UnityEditor;
using UnityEngine;

namespace thot.DS.Adapters {
    public class FileSystemGraph {
        private DSGraphView graphView;
        private const string containerFolderPath = "Assets/DialogueSystem/Dialogues";


        private static Dictionary<string, DSDialogueSO> createdDialogues = new Dictionary<string, DSDialogueSO>();

        public FileSystemGraph(DSGraphView dsGraphView) {
            this.graphView = dsGraphView;
        }


        public void Save(string filename) {
            Debug.Log(filename);
            CreateStaticFolders();

            DSGraphSaveDataSO graphData =
                Assets.CreateAsset<DSGraphSaveDataSO>("Assets/DialogueSystem/Dialogues/Graphs", $"{filename}Graph");
            graphData.Initialize(filename);

            SaveNodes(graphData);


            Assets.SaveAsset(graphData);
        }

        private static void CreateStaticFolders() {
            Assets.CreateFolder("Assets", "DialogueSystem");
            Assets.CreateFolder("Assets/DialogueSystem", "Dialogues");
            Assets.CreateFolder("Assets/DialogueSystem/Dialogues", "Graphs");
            Assets.CreateFolder(containerFolderPath, "Global");
            Assets.CreateFolder($"{containerFolderPath}/Global", "Dialogues");
            // CreateFolder(containerFolderPath, "Groups");
        }

        private void SaveNodes(DSGraphSaveDataSO graphData) {
            var nodes = graphView.GetNodes();
            graphData.AddNodes(nodes);

            foreach (var node in nodes) {
                SaveNodeToScriptableObject(node);
            }

            UpdateDialogChoicesConnections(nodes);
        }

        private static void SaveNodeToScriptableObject(DSNode node) {
            DSDialogueSO dialogue;

            /*if (node.Group != null) {
                dialogue = CreateAsset<DSDialogueSO>($"{containerFolderPath}/Groups/{node.Group.title}/Dialogues",
                    node.DialogueName);
                dialogueContainer.DialogueGroups.AddItem(createdDialogueGroups[node.Group.ID], dialogue);
            }
            else {
                dialogue = CreateAsset<DSDialogueSO>($"{containerFolderPath}/Global/Dialogues", node.DialogueName);

                dialogueContainer.UngroupedDialogues.Add(dialogue);
            }
            );*/
            dialogue = Assets.CreateAsset<DSDialogueSO>($"{containerFolderPath}/Global/Dialogues", node.DialogueName);
            dialogue.Initialize(
                node.DialogueName,
                node.DialogueText,
                FromNodeChoices(node.Choices),
                node.DialogueType,
                node.IsStartingNode()
            );

            createdDialogues.Add(node.ID, dialogue);
            Assets.SaveAsset(dialogue);
        }

        private static List<DSDialogueSO.DSDialogueChoiceData> FromNodeChoices(List<DSChoice> nodeChoices) {
            return nodeChoices.Select(node => new DSDialogueSO.DSDialogueChoiceData {
                    Text = node.Text
                }
            ).ToList();
        }

        private static void UpdateDialogChoicesConnections(List<DSNode> nodes) {
            foreach (DSNode node in nodes) {
                DSDialogueSO dialogue = createdDialogues[node.ID];

                for (int choiceIndex = 0; choiceIndex < node.Choices.Count; ++choiceIndex) {
                    var nodeChoice = node.Choices[choiceIndex];

                    if (string.IsNullOrEmpty(nodeChoice.NodeID)) {
                        continue;
                    }

                    dialogue.Choices[choiceIndex].NextDialogue = createdDialogues[nodeChoice.NodeID];
                }

                Assets.SaveAsset(dialogue);
            }
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public void Load(string fileNameWithoutExtension) {
            Debug.Log(fileNameWithoutExtension);
        }
    }
}
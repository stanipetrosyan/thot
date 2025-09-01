using System;
using System.Collections.Generic;
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
            // CreateFolder(containerFolderPath, "Global");
            // CreateFolder(containerFolderPath, "Groups");
            // CreateFolder($"{containerFolderPath}/Global", "Dialogues");
        }

        private void SaveNodes(DSGraphSaveDataSO graphData) {
            var nodes = graphView.GetNodes();
            graphData.AddNodes(nodes);

            foreach (var node in nodes) {
                SaveNodeToScriptableObject(node);
            }

            // UpdateDialogChoicesConnections();
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

            dialogue.Initialize(
                node.DialogueName,
                node.Text,
                ConvertNodeChoicesToDialogueChoices(node.Choices),
                node.DialogueType,
                node.IsStartingNode()
            );*/
            dialogue = Assets.CreateAsset<DSDialogueSO>($"{containerFolderPath}/Global/Dialogues", node.DialogueName);
            dialogue.Initialize(
                node.DialogueName,
                node.DialogueText,
                ConvertNodeChoicesToDialogueChoices(node.Choices),
                node.DialogueType,
                node.IsStartingNode()
            );
            //createdDialogues.Add(node.ID, dialogue);
            Assets.SaveAsset(dialogue);
        }

        private static List<DSDialogueSO.DSDialogueChoiceData> ConvertNodeChoicesToDialogueChoices(
            List<DSChoice> nodeChoices) {
            List<DSDialogueSO.DSDialogueChoiceData> dialogueChoices = new List<DSDialogueSO.DSDialogueChoiceData>();

            foreach (var nodeChoice in nodeChoices) {
                DSDialogueSO.DSDialogueChoiceData choiceData = new DSDialogueSO.DSDialogueChoiceData() {
                    Text = nodeChoice.Text
                };

                dialogueChoices.Add(choiceData);
            }

            return dialogueChoices;
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public void Load(string fileNameWithoutExtension) {
            Debug.Log(fileNameWithoutExtension);
        }
    }
}
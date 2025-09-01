using System;
using thot.DS.Windows;
using UnityEditor;
using UnityEngine;

namespace thot.DS.Adapters {
    public class FileSystemGraph {
        private DSGraphView graphView;

        public FileSystemGraph(DSGraphView dsGraphView) {
            this.graphView = dsGraphView;
        }
        
        
        public void Save(string path) {
            Debug.Log(path);
            CreateStaticFolders();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public void Load(string fileNameWithoutExtension) {
            Debug.Log(fileNameWithoutExtension);
        }
        
        
        private static void CreateStaticFolders() {
            CreateFolder("Assets", "DialogueSystem");
            CreateFolder("Assets/DialogueSystem", "Dialogues");
            CreateFolder("Assets/DialogueSystem/Dialogues", "Graphs");
           // CreateFolder(containerFolderPath, "Global");
           // CreateFolder(containerFolderPath, "Groups");
           // CreateFolder($"{containerFolderPath}/Global", "Dialogues");
        }

        private static void CreateFolder(string path, string folderName) {
            if (AssetDatabase.IsValidFolder(path + "/" + folderName)) {
                return;
            }

            AssetDatabase.CreateFolder(path, folderName);
        }
    }
}
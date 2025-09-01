using System;
using UnityEngine;

namespace thot.DS.Adapters {
    public class FileSystemGraph {
        public void Save(string path) {
            Debug.Log(path);
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public void Load(string fileNameWithoutExtension) {
            Debug.Log(fileNameWithoutExtension);
        }
    }
}
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    partial class LocationEditor
    {
        private string _newLocationName;
        
        /// <summary> Доступно ли такое имя для сцены </summary>
        private bool ValidLocationName
        {
            get
            {
                var path = Path.Combine(PathConfig.ScenesFolder,
                    $"{_newLocationName}.unity");
                
                if (!File.Exists(path))
                    return true;

                EditorGUILayout.HelpBox("Scene with the same name already exists!", MessageType.Error);
                return false;
            }
        }

        private void CreateSceneOnGUI()
        {
            _newLocationName = EditorGUILayout.TextField("Level name: ", _newLocationName);

            GUILayout.Space(10);

            if (ValidLocationName)
            {
                if (GUILayout.Button("Create", GUIStyles.GreenButton))
                {
                    LevelCreator.Create(_newLocationName);
                    CurrentState = EditorState.Editing;
                }
            }

            if (GUILayout.Button("Back", GUIStyles.RedButton))
                CurrentState = EditorState.Loading;
        }

    }

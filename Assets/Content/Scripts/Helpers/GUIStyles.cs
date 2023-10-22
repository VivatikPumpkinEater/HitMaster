using UnityEditor;
using UnityEngine;

public static class GUIStyles
    {
        public static readonly GUIStyle MiniButton = new GUIStyle(EditorStyles.miniButton)
        {
            fixedWidth = 30,
            fixedHeight = 19,
            padding = new RectOffset(0, 0, 0, 0),
        };
        
        public static readonly GUIStyle TitleLabel = new(EditorStyles.label)
        {
            normal = { textColor = new Color(0.76f, 0.61f, 0.38f) }, alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold, fontSize = 13
        };
        
        public static readonly GUIStyle GreenButton = new(GUI.skin.button)
        {
            normal = { textColor = Color.green }
        };
        
        public static readonly GUIStyle YellowButton = new(GUI.skin.button)
        {
            normal = { textColor = Color.yellow }
        };
        
        public static readonly GUIStyle RedButton = new(GUI.skin.button)
        {
            normal = { textColor = new Color(1f, 0.44f, 0.38f) }
        };

        public static readonly GUIStyle WindowTitleStyle = new GUIStyle(EditorStyles.label)
        {
            normal = { textColor = new Color(0.0f, 0.76f, 0.02f) },
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 14
        };
        
        public static readonly GUIStyle YellowButtonTwoMarginsHigh = new(GUI.skin.button)
        {
            normal = { textColor = Color.yellow },
            fixedHeight = 38
        };
    }

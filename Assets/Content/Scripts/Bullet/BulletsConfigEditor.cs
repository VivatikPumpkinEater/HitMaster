using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletsConfig))]
public class BulletsConfigEditor : Editor
{
    private BulletsConfig _target;
    
    private void OnEnable()
    {
        _target = (BulletsConfig)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        CheckDuplicate();

        if (GUILayout.Button("Generate types", GUILayout.Height(40)))
            _target.GenerateTypes();
    }

    private void CheckDuplicate()
    {
        var duplicateItems = _target.Models
            .GroupBy(x => x.Type)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        if(duplicateItems.Any())
            EditorGUILayout.HelpBox("There's a duplicate", MessageType.Warning);
    }

}
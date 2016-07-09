using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MatAnimHelper))]
public class MatAnimHelperEditorExtension : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Init color"))
        {
            if (target.GetType() == typeof(MatAnimHelper))
            {
                MatAnimHelper animHelper = (MatAnimHelper)target;
                animHelper.projectorMat = animHelper.mMat;
            }
        }

    }


}

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Controls)), CanEditMultipleObjects]
public class ControlsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Controls ctrl = (Controls)target;
       //ctrl.move = EditorGUILayout.ObjectField("Events Reference: ", ctrl.move, typeof(VRControllerEvents), true) as VRControllerEvents;
        EditorGUILayout.Space();
     //   ctrl.rig = EditorGUILayout.ObjectField("Camera Rig: ", ctrl.rig, typeof(Transform), true) as Transform;
       // ctrl.headset = EditorGUILayout.ObjectField("Headset: ", ctrl.headset, typeof(Transform), true) as Transform;
        //ctrl.groundCheck = EditorGUILayout.ObjectField("Ground Check: ", ctrl.groundCheck, typeof(Grounding), true) as Grounding;
        EditorGUILayout.Space();
        ctrl.menuLeft = EditorGUILayout.ObjectField("Left Menu Controls: ", ctrl.menuLeft, typeof(GameObject), true) as GameObject;
        ctrl.menuRight = EditorGUILayout.ObjectField("Right Menu Controls: ", ctrl.menuRight, typeof(GameObject), true) as GameObject;
        EditorGUILayout.Space();
        ctrl.gameLeft = EditorGUILayout.ObjectField("Left Game Controls: ", ctrl.gameLeft, typeof(GameObject), true) as GameObject;
        ctrl.gameRight = EditorGUILayout.ObjectField("Right Game Controls: ", ctrl.gameRight, typeof(GameObject), true) as GameObject;
        EditorGUILayout.Space();
        ctrl.walking = EditorGUILayout.ObjectField("Walking Sound: ", ctrl.walking, typeof(AudioSource), true) as AudioSource;
        ctrl.running = EditorGUILayout.ObjectField("Running Sound: ", ctrl.running, typeof(AudioSource), true) as AudioSource;
        ctrl.jumping = EditorGUILayout.ObjectField("Jumping Sound: ", ctrl.jumping, typeof(AudioSource), true) as AudioSource;
    }
}
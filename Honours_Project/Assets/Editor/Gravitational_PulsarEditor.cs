using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Gravitational_Pulsar)), CanEditMultipleObjects]
public class Gravitational_PulsarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Gravitational_Pulsar gp = (Gravitational_Pulsar)target;
        EditorGUILayout.Space();
        gp.gp_ReferencePoint = EditorGUILayout.ObjectField("Reference Point: ", gp.gp_ReferencePoint, typeof(Transform), true) as Transform;
        gp.gp_LayerMask = EditorGUILayout.LayerField("Layer Mask: ", gp.gp_LayerMask);
        EditorGUILayout.Space();
        gp.forceOverDist = EditorGUILayout.CurveField("Animation Curve: ", gp.forceOverDist);
        gp._Animator = EditorGUILayout.ObjectField("Animator: ", gp._Animator, typeof(Animator), true) as Animator;
        EditorGUILayout.Space();
        gp.gp_Grab = EditorGUILayout.ObjectField("Grab Sound: ", gp.gp_Grab, typeof(AudioSource), true) as AudioSource;
        gp.gp_Carry = EditorGUILayout.ObjectField("Carry Sound: ", gp.gp_Carry, typeof(AudioSource), true) as AudioSource;
        gp.gp_Drop = EditorGUILayout.ObjectField("Drop Sound: ", gp.gp_Drop, typeof(AudioSource), true) as AudioSource;
        gp.gp_Fire = EditorGUILayout.ObjectField("Fire Sound: ", gp.gp_Fire, typeof(AudioSource), true) as AudioSource;
    }
}

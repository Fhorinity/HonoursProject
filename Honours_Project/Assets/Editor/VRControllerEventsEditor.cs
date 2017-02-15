using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VRControllerEvents)), CanEditMultipleObjects]
public class VRControllerEventsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        VRControllerEvents vrce = (VRControllerEvents)target;
        vrce.useMovementControls = EditorGUILayout.Toggle("Use Movement Controls: ", vrce.useMovementControls);
        if (vrce.useMovementControls)
        {
            EditorGUILayout.Space();
            vrce.rig = EditorGUILayout.ObjectField("Rig: ", vrce.rig, typeof(Transform), true) as Transform;
          //  vrce.headset = EditorGUILayout.ObjectField("Headset: ", vrce.headset, typeof(Transform), true) as Transform;
            EditorGUILayout.Space();
            vrce.walking = EditorGUILayout.ObjectField("Walking Sound Effect: ", vrce.walking, typeof(AudioSource), true) as AudioSource;
            vrce.running = EditorGUILayout.ObjectField("Running Sound Effect: ", vrce.running, typeof(AudioSource), true) as AudioSource;
            vrce.jumping = EditorGUILayout.ObjectField("Jumping Sound Effect: ", vrce.jumping, typeof(AudioSource), true) as AudioSource;
            EditorGUILayout.Space();
            vrce.groundCheck = EditorGUILayout.ObjectField("Ground Check: ", vrce.groundCheck, typeof(Grounding), true) as Grounding;
            EditorGUILayout.Space();
        }
        vrce.useGravitationalPulsar = EditorGUILayout.Toggle("Use Gravitational Pulsar: ", vrce.useGravitationalPulsar);
        if (vrce.useGravitationalPulsar)
        {
            EditorGUILayout.Space();
            vrce.rigArea = EditorGUILayout.ObjectField("Rig Area: ", vrce.rigArea, typeof(GameObject), true) as GameObject;
            vrce.gp_ReferencePoint = EditorGUILayout.ObjectField("Reference Point: ", vrce.gp_ReferencePoint, typeof(Transform), true) as Transform;
            EditorGUILayout.Space();
            vrce.speedMultiplier = EditorGUILayout.FloatField("Speed Multiplier: ", vrce.speedMultiplier);
            vrce.maxYDim = EditorGUILayout.FloatField("Max Y Dim: ", vrce.maxYDim);
            vrce.grabDistance = EditorGUILayout.FloatField("Max Grab Distance: ", vrce.grabDistance);
            EditorGUILayout.Space();
            vrce.forceOverDist = EditorGUILayout.CurveField("Force Over Distance: ", vrce.forceOverDist);
            vrce.gp_LayerMask = EditorGUILayout.LayerField("Layer Mask: ", vrce.gp_LayerMask);
            EditorGUILayout.Space();
            vrce.gp_Grab = EditorGUILayout.ObjectField("Grab Sound Effect: ", vrce.gp_Grab, typeof(AudioSource), true) as AudioSource;
            vrce.gp_Carry = EditorGUILayout.ObjectField("Carry Sound Effect: ", vrce.gp_Carry, typeof(AudioSource), true) as AudioSource;
            vrce.gp_Drop = EditorGUILayout.ObjectField("Drop Sound Effect: ", vrce.gp_Drop, typeof(AudioSource), true) as AudioSource;
            vrce.gp_Fire = EditorGUILayout.ObjectField("Fire Sound Effect: ", vrce.gp_Fire, typeof(AudioSource), true) as AudioSource;
            EditorGUILayout.Space();
            vrce._Animator = EditorGUILayout.ObjectField("Animator: ", vrce._Animator, typeof(Animator), true) as Animator;
            EditorGUILayout.Space();
        }
        vrce.usePlasmaticGrappler = EditorGUILayout.Toggle("Use Plasmatic Grappler: ", vrce.usePlasmaticGrappler);
        if (vrce.usePlasmaticGrappler)
        {
            EditorGUILayout.Space();
            vrce.pg_ReferencePoint = EditorGUILayout.ObjectField("Reference Point: ", vrce.pg_ReferencePoint, typeof(Transform), true) as Transform;
            vrce.pg_LineRender = EditorGUILayout.ObjectField("Line Reader: ", vrce.pg_LineRender, typeof(LineRenderer), true) as LineRenderer;
            EditorGUILayout.Space();
            vrce.speed = EditorGUILayout.FloatField("Speed: ", vrce.speed);
            vrce.maxDistance = EditorGUILayout.IntField("Max Grab Distance: ", vrce.maxDistance);
            EditorGUILayout.Space();
            vrce.position = EditorGUILayout.Vector3Field("Position: ", vrce.position);
            vrce.pg_LayerMask = EditorGUILayout.LayerField("Layer Mask: ", vrce.pg_LayerMask);
            vrce.isFlying = EditorGUILayout.Toggle("Flying: ", vrce.isFlying);
            EditorGUILayout.Space();
            vrce.pg_Fire = EditorGUILayout.ObjectField("Fire Sound Effect: ", vrce.pg_Fire, typeof(AudioSource), true) as AudioSource;
            vrce.pg_Swing = EditorGUILayout.ObjectField("Swing Sound Effect: ", vrce.pg_Swing, typeof(AudioSource), true) as AudioSource;
            vrce.pg_Release = EditorGUILayout.ObjectField("Release Sound Effect: ", vrce.pg_Release, typeof(AudioSource), true) as AudioSource;
            EditorGUILayout.Space();
            vrce._Animator = EditorGUILayout.ObjectField("Animator: ", vrce._Animator, typeof(Animator), true) as Animator;
            EditorGUILayout.Space();
        }      
    }
}
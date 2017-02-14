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
            vrce.rig = EditorGUILayout.ObjectField("Rig Area: ", vrce.rig, typeof(Transform), true) as Transform;
          //  vrce.headset = EditorGUILayout.ObjectField("Headset: ", vrce.headset, typeof(Transform), true) as Transform;
            EditorGUILayout.Space();
            vrce.walking = EditorGUILayout.ObjectField("Walking Sound Effect: ", vrce.walking, typeof(AudioSource), true) as AudioSource;
            vrce.running = EditorGUILayout.ObjectField("Running Sound Effect: ", vrce.running, typeof(AudioSource), true) as AudioSource;
            vrce.jumping = EditorGUILayout.ObjectField("Jumping Sound Effect: ", vrce.jumping, typeof(AudioSource), true) as AudioSource;
            EditorGUILayout.Space();
        }
        vrce.useGravitationalPulsar = EditorGUILayout.Toggle("Use Gravitational Pulsar: ", vrce.useGravitationalPulsar);
        if (vrce.useGravitationalPulsar)
        {
            EditorGUILayout.Space();
            vrce.rigArea = EditorGUILayout.ObjectField("Rig Area: ", vrce.rigArea, typeof(GameObject), true) as GameObject;
            vrce.holdPosition = EditorGUILayout.ObjectField("Reference Point: ", vrce.holdPosition, typeof(Transform), true) as Transform;
            EditorGUILayout.Space();
            vrce.speedMultiplier = EditorGUILayout.FloatField("Speed Multiplier: ", vrce.speedMultiplier);
            vrce.maxYDim = EditorGUILayout.FloatField("Max Y Dim: ", vrce.maxYDim);
            vrce.grabDistance = EditorGUILayout.FloatField("Max Grab Distance: ", vrce.grabDistance);
            EditorGUILayout.Space();
            vrce.forceOverDist = EditorGUILayout.CurveField("Force Over Distance: ", vrce.forceOverDist);
            vrce.layerMask = EditorGUILayout.LayerField("Layer Mask: ", vrce.layerMask);
            EditorGUILayout.Space();
            vrce.gp_Grab = EditorGUILayout.ObjectField("Gravitational Pulsar Grab Sound Effect: ", vrce.gp_Grab, typeof(AudioSource), true) as AudioSource;
            vrce.gp_Carry = EditorGUILayout.ObjectField("Gravitational Pulsar Carry Sound Effect: ", vrce.gp_Carry, typeof(AudioSource), true) as AudioSource;
            vrce.gp_Drop = EditorGUILayout.ObjectField("Gravitational Pulsar Drop Sound Effect: ", vrce.gp_Drop, typeof(AudioSource), true) as AudioSource;
            vrce.gp_Fire = EditorGUILayout.ObjectField("Gravitational Pulsar Fire Sound Effect: ", vrce.gp_Fire, typeof(AudioSource), true) as AudioSource;
            EditorGUILayout.Space();
        }
        vrce.usePlasmaticGrappler = EditorGUILayout.Toggle("Use Plasmatic Grappler: ", vrce.usePlasmaticGrappler);
        if (vrce.usePlasmaticGrappler)
        {
            EditorGUILayout.Space();
            vrce.referencePoint = EditorGUILayout.ObjectField("Reference Point: ", vrce.referencePoint, typeof(Transform), true) as Transform;
            vrce.lr = EditorGUILayout.ObjectField("Line Reader: ", vrce.lr, typeof(LineRenderer), true) as LineRenderer;
            EditorGUILayout.Space();
            vrce.speed = EditorGUILayout.FloatField("Speed: ", vrce.speed);
            vrce.maxDistance = EditorGUILayout.IntField("Max Grab Distance: ", vrce.maxDistance);
            EditorGUILayout.Space();
            vrce.position = EditorGUILayout.Vector3Field("Position: ", vrce.position);
            vrce.cullingmask = EditorGUILayout.LayerField("Layer Mask: ", vrce.cullingmask);
            vrce.isFlying = EditorGUILayout.Toggle("Flying: ", vrce.isFlying);
            EditorGUILayout.Space();
            vrce.pg_Fire = EditorGUILayout.ObjectField("Plasmatic Grapple Fire Sound Effect: ", vrce.pg_Fire, typeof(AudioSource), true) as AudioSource;
            vrce.pg_Swing = EditorGUILayout.ObjectField("Plasmatic Grapple Swing Sound Effect: ", vrce.pg_Swing, typeof(AudioSource), true) as AudioSource;
            vrce.pg_Release = EditorGUILayout.ObjectField("Plasmatic Grapple Release Sound Effect: ", vrce.pg_Release, typeof(AudioSource), true) as AudioSource;
            EditorGUILayout.Space();
        }      
    }
}
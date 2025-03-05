using UnityEditor;
using UnityEngine;

namespace WinterUniverse
{
    [CustomEditor(typeof(PawnController))]
    public class CE_PawnController : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            PawnController pawn = (PawnController)target;
            //GUILayout.Label("Name => " + pawn.Agent.Data.DisplayName);
            //GUILayout.Label("Health => " + pawn.Agent.Pawn.HealthSystem.HealthPercent * 100f + "%");
            GUILayout.Label("===== Equipment =====");
            if (pawn.Equipment != null)
            {
                foreach (ArmorSlot slot in pawn.Equipment.ArmorSlots)
                {
                    GUILayout.Label($"{slot.Type.DisplayName}: {(slot.Config != null ? slot.Config.DisplayName : "Empty")}");
                }
            }
            GUILayout.Label("===== Inventory =====");
            if (pawn.Inventory != null)
            {
                foreach (ItemConfig item in pawn.Inventory.Items)
                {
                    GUILayout.Label($"{item.DisplayName}");
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
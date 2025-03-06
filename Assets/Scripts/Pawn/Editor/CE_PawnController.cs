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
            //serializedObject.Update();
            PawnController pawn = (PawnController)target;
            if (pawn.Created)
            {
                GUILayout.Label("===== Basic Info =====");
                if (pawn.Config != null)
                {
                    GUILayout.Label($"Name => {pawn.Config.DisplayName}");
                }
                GUILayout.Label("===== Status =====");
                if (pawn.Status != null)
                {
                    GUILayout.Label($"Health => {pawn.Status.HealthPercent * 100f}%");
                    GUILayout.Label($"Energy => {pawn.Status.EnergyPercent * 100f}%");
                    GUILayout.Label($"Mana => {pawn.Status.ManaPercent * 100f}%");
                }
                GUILayout.Label("===== Stats =====");
                if (pawn.Status != null)
                {
                    foreach (Stat s in pawn.Status.Stats)
                    {
                        GUILayout.Label($"{s.Config.DisplayName}: {s.CurrentValue:0.##}{(s.Config.IsPercent ? "%" : "")}");
                    }
                }
                GUILayout.Label("===== Locomotion =====");
                if (pawn.Locomotion != null)
                {
                    if (pawn.Locomotion.Target != null)
                    {
                        GUILayout.Label($"Is Following Target");
                    }
                    if (pawn.Locomotion.ReachedDestination)
                    {
                        GUILayout.Label($"Reached Destination");
                    }
                    else
                    {
                        GUILayout.Label($"Remaining Distance => {pawn.Locomotion.RemainingDistance}");
                    }
                }
                GUILayout.Label("===== Equipment =====");
                if (pawn.Equipment != null)
                {
                    GUILayout.Label($"Weapon: {(pawn.Equipment.WeaponSlot.Config != null ? pawn.Equipment.WeaponSlot.Config.DisplayName : "Empty")}");
                    foreach (ArmorSlot slot in pawn.Equipment.ArmorSlots)
                    {
                        GUILayout.Label($"{slot.Type.DisplayName}: {(slot.Config != null ? slot.Config.DisplayName : "Empty")}");
                    }
                }
                GUILayout.Label("===== Inventory =====");
                if (pawn.Inventory != null)
                {
                    foreach (ItemStack stack in pawn.Inventory.Stacks)
                    {
                        GUILayout.Label($"{stack.Item.DisplayName}: {stack.Amount}");
                    }
                }
            }
            //serializedObject.ApplyModifiedProperties();
        }
    }
}
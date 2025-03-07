using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace WinterUniverse
{
    [CustomEditor(typeof(NPCController))]
    public class CE_NPCController : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            NPCController npc = (NPCController)target;
            GUILayout.Label($"Current Goal => {(npc.CurrentGoal != null ? npc.CurrentGoal.Config.DisplayName : "Empty")}");
            GUILayout.Label($"Current Action => {(npc.CurrentAction != null ? npc.CurrentAction.Config.DisplayName : "Empty")}");
            GUILayout.Label("===== Goals =====");
            if (npc.Goals.Count > 0)
            {
                var sortedGoals = from entry in npc.Goals orderby entry.Value descending select entry;
                foreach (KeyValuePair<GoalHolder, int> goal in sortedGoals)
                {
                    GUILayout.Label($"{goal.Key.Config.DisplayName} : {goal.Value}");
                }
            }
            foreach (ActionBase action in npc.Actions)
            {
                GUILayout.Label("===== Possible Action =====");
                string condition = "";
                string effect = "";
                foreach (KeyValuePair<string, bool> cond in action.Conditions)
                {
                    condition += $"{cond.Key} {cond.Value}, ";
                }
                foreach (KeyValuePair<string, bool> eff in action.Effects)
                {
                    effect += $"{eff.Key} {eff.Value}, ";
                }
                GUILayout.Label($"== DO ==\n[{action.Config.DisplayName}]\n== WHEN ==\n[{condition}]\n== AND NEED ==\n[{effect}]");
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
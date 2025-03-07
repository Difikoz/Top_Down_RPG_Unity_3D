using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class NPCManager : MonoBehaviour
    {
        private List<NPCController> _controllers = new();

        public void Initialize()
        {
            NPCController[] controllers = FindObjectsByType<NPCController>(FindObjectsSortMode.None);
            foreach (NPCController controller in controllers)
            {
                AddController(controller);
            }
        }

        public void ResetComponent()
        {
            foreach (NPCController controller in _controllers)
            {
                controller.ResetComponent();
            }
        }

        public void OnUpdate()
        {
            foreach (NPCController controller in _controllers)
            {
                controller.OnUpdate();
            }
        }

        public void AddController(NPCController controller)
        {
            _controllers.Add(controller);
            controller.Initialize();
        }
    }
}
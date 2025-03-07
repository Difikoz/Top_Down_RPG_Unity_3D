using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WinterUniverse
{
    public class NPCController : MonoBehaviour
    {
        private PawnController _pawn;
        private ActionBase _currentAction;
        private GoalHolder _currentGoal;
        private List<ActionBase> _actions = new();
        private Dictionary<GoalHolder, int> _goals = new();
        private Queue<ActionBase> _actionQueue;

        [SerializeField] private PawnConfig _config;
        [SerializeField] private GoalHolderConfig _goalCreatorHolder;

        public PawnController Pawn => _pawn;

        public ActionBase CurrentAction => _currentAction;
        public GoalHolder CurrentGoal => _currentGoal;
        public List<ActionBase> Actions => _actions;
        public Dictionary<GoalHolder, int> Goals => _goals;

        public void Initialize()
        {
            _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(transform);
            _pawn.Initialize(_config);
            ActionBase[] actions = GetComponentsInChildren<ActionBase>();
            foreach (ActionBase action in actions)
            {
                _actions.Add(action);
                action.Initialize();
            }
            foreach (GoalCreator creator in _goalCreatorHolder.GoalsToAdd)
            {
                _goals.Add(new(creator.Config), creator.Priority);
            }
        }

        public void ResetComponent()
        {
            _pawn.ResetComponent();
        }

        public void OnUpdate()
        {
            _pawn.OnUpdate();
            //transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.rotation);
            //if (_pawn.Combat.Target == null && _pawn.Detection.DetectedPawns.Count > 0)
            //{
            //    _pawn.Combat.SetTarget(_pawn.Detection.GetClosestPawn());
            //}
            ProccessGOAP();
        }

        private void ProccessGOAP()
        {
            if (_currentAction != null)
            {
                if (_currentAction.CanAbort())
                {
                    _currentAction.OnAbort();
                    _currentAction = null;
                }
                else if (_currentAction.CanComplete())
                {
                    _currentAction.OnComplete();
                    _currentAction = null;
                }
                else
                {
                    _currentAction.OnUpdate(Time.deltaTime);
                }
                return;
            }
            if (_actionQueue != null)
            {
                if (_actionQueue.Count > 0)
                {
                    _currentAction = _actionQueue.Dequeue();
                    if (_currentAction.CanStart())
                    {
                        _currentAction.OnStart();
                        return;
                    }
                    else
                    {
                        _actionQueue = null;
                    }
                }
                if (_currentGoal != null)
                {
                    if (!_currentGoal.Config.Repeatable)
                    {
                        _goals.Remove(_currentGoal);
                    }
                    _actionQueue = null;
                }
            }
            if (_actionQueue == null)
            {
                var sortedGoals = from entry in _goals orderby entry.Value descending select entry;
                foreach (KeyValuePair<GoalHolder, int> sg in sortedGoals)
                {
                    //Debug.LogWarning($"Try Get Plan for [{sg.Key.GoalName}]");
                    _actionQueue = GameManager.StaticInstance.TaskManager.GetPlan(_actions, _pawn.StateHolder.States, _goals.ElementAt(0).Key.Conditions);
                    if (_actionQueue != null)
                    {
                        _currentGoal = sg.Key;
                        return;
                    }
                }
            }
        }
    }
}
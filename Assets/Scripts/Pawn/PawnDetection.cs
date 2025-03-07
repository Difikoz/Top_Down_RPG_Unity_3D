using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnDetection : MonoBehaviour
    {
        private PawnController _pawn;
        private List<PawnController> _detectedPawns = new();
        private List<InteractableBase> _detectedInteractables = new();
        private float _detectionTime;

        [SerializeField] private float _detectionCooldown = 2f;

        public List<PawnController> DetectedPawns => _detectedPawns;
        public List<InteractableBase> DetectedInteractables => _detectedInteractables;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void OnUpdate()
        {
            if (_detectionTime >= _detectionCooldown)
            {
                _detectedPawns.Clear();
                _detectedInteractables.Clear();
                Collider[] colliders = Physics.OverlapSphere(_pawn.Animator.EyesPoint.position, _pawn.Status.ViewDistance.CurrentValue, GameManager.StaticInstance.LayerManager.DetectableMask);
                foreach (Collider collider in colliders)
                {
                    if (!TargetInRange(collider.transform))
                    {
                        continue;
                    }
                    InteractableBase interactable = collider.GetComponentInParent<InteractableBase>();
                    if (interactable != null /*&& interactable.AllowInteractionForNPC*/ && interactable.CanInteract(_pawn))
                    {
                        _detectedInteractables.Add(interactable);
                    }
                    else if (collider.TryGetComponent(out interactable) /*&& interactable.AllowInteractionForNPC */&& interactable.CanInteract(_pawn))
                    {
                        _detectedInteractables.Add(interactable);
                    }
                    else if (collider.TryGetComponent(out PawnController pawn) && pawn != _pawn && !pawn.Status.IsDead && TargetIsVisible(pawn))
                    {
                        _detectedPawns.Add(pawn);
                    }
                }
                _detectionTime = 0f;
            }
            else
            {
                _detectionTime += Time.deltaTime;
            }
        }

        public bool TargetInRange(Transform t)
        {
            float distance = Vector3.Distance(transform.position, t.position);
            if (distance <= _pawn.Status.HearRadius.CurrentValue)
            {
                return true;
            }
            else if (distance <= _pawn.Status.ViewDistance.CurrentValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TargetIsVisible(Transform t)
        {
            Vector3 direction = (t.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, direction) > _pawn.Status.ViewAngle.CurrentValue / 2f)
            {
                return false;
            }
            if (Physics.Linecast(transform.position, t.position, GameManager.StaticInstance.LayerManager.ObstacleMask))
            {
                return false;
            }
            return true;
        }

        public bool TargetIsVisible(PawnController pawn)
        {
            Vector3 direction = (pawn.Animator.BodyPoint.position - _pawn.Animator.EyesPoint.position).normalized;
            if (Vector3.Angle(_pawn.Animator.EyesPoint.forward, direction) > _pawn.Status.ViewAngle.CurrentValue / 2f)
            {
                return false;
            }
            if (Physics.Linecast(_pawn.Animator.EyesPoint.position, pawn.Animator.BodyPoint.position, GameManager.StaticInstance.LayerManager.ObstacleMask))
            {
                return false;
            }
            return true;
        }

        public PawnController GetClosestPawn()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in _detectedPawns)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }

        public PawnController GetClosestEnemy()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in _detectedPawns)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }

        public PawnController GetClosestNeutral()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in _detectedPawns)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }

        public PawnController GetClosestAlly()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in _detectedPawns)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }
    }
}
using System;
using UnityEngine;

namespace HunterAllen.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        public PlayerMover _playerMover;

        [SerializeField]
        public PlayerCamera _playerCamera;

        public virtual void ProvideMoveInput(Vector2 input)
        {
            _playerMover.SetMoveInput(input);
        }
        public virtual void ProvideLookInput(Vector2 input)
        {
            _playerCamera.ApplyLookInput(input);
        }
        public virtual void ProvideSprintInput(bool input)
        {
            _playerMover.SetSprint(input);
        }
        public virtual void ProvideCrouchInput(bool input)
        {
            _playerMover.SetCrouch(input);
        }
    }
}
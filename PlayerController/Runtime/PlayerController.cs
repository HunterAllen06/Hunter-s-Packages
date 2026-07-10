using System;
using UnityEngine;

namespace HunterAllen.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        PlayerMover _playerMover;

        [SerializeField]
        PlayerCamera _playerCamera;

        public void ProvideMoveInput(Vector2 input)
        {
            _playerMover.SetMoveInput(input);
        }
        public void ProvideLookInput(Vector2 input)
        {
            _playerCamera.ApplyLookInput(input);
        }
        public void ProvideSprintInput(bool input)
        {
            _playerMover.SetSprint(input);
        }
        public void ProvideCrouchInput(bool input)
        {
            _playerMover.SetCrouch(input);
        }
    }
}
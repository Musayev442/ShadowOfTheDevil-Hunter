using System.Collections;
using UnityEngine;

namespace App.Code.Movement.Interfaces
{
    public interface IJumper
    {
        void Jump(float force, bool isGrounded);
        void UpdateJumpBuffer(bool isJumping);
        float jumpStartTime { get; set; }
    }
}
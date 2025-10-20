using UnityEngine;

namespace App.Code.Core.Systems.AnimationSystem
{
    [System.Serializable]
    public class AnimationConfig
    {
        [Header("Animator Parameters")]
        public string moveSpeedParam = "MoveSpeed";
        public string isGroundedParam = "IsGrounded";
        public string jumpTriggerParam = "JumpTrigger";
        public string attackTriggerParam = "AttackTrigger";
        public string attackIndexParam = "AttackIndex";
        public string isAttackingParam = "IsAttacking";
    
        [Header("Lock-On Parameters")]
        public string isLockedOnParam = "IsLockedOn";
        public string horizontalParam = "Horizontal";
        public string verticalParam = "Vertical";
        
        [Header("Blend Tree Values")]
        public float idleSpeed = 0f;
        public float walkSpeed = 1f;
        public float runSpeed = 2f;
        public float sprintSpeed = 3f;
        
    
        [Header("Smoothing")]
        public float blendSpeed = 10f;
        public float lockOnBlendSpeed = 15f;
    }
}
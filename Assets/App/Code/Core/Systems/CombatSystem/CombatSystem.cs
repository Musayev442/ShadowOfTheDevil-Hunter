using App.Code.Core.Systems.CombatSystem.Interfaces;
using UnityEngine;

namespace App.Code.Core.Systems.CombatSystem
{
    public class CombatSystem : ICombatSystem
    {
        private CombatConfig _config;

        // Attack state
        private bool _isAttacking;
        private int _currentAttackIndex; // 0 = Attack1, 1 = Attack2, 2 = Attack3
        private float _attackTimer;
        private float _cooldownTimer;

        // Combo tracking
        private bool _nextAttackQueued;
        private bool _inComboWindow;

        public CombatSystem(CombatConfig config)
        {
            _config = config;
            _isAttacking = false;
            _currentAttackIndex = -1;
            _attackTimer = 0f;
            _cooldownTimer = 0f;
            _nextAttackQueued = false;
            _inComboWindow = false;
        }

        public void Attack()
        {
            // If in combo window, queue next attack
            if (_inComboWindow && _isAttacking)
            {
                _nextAttackQueued = true;
                Debug.Log($"[AttackSystem] Queued next attack! Current: {_currentAttackIndex}");
                return;
            }

            // Start new attack if not attacking and cooldown finished
            if (!_isAttacking && _cooldownTimer <= 0f)
            {
                StartAttack(0); // Start with Attack1
            }
        }

        public void UpdateAttack()
        {
            // Update cooldown timer
            if (_cooldownTimer > 0f)
            {
                _cooldownTimer -= Time.deltaTime;
            }

            // Update attack state
            if (_isAttacking)
            {
                _attackTimer += Time.deltaTime;

                float currentAttackDuration = GetCurrentAttackDuration();
                float normalizedTime = _attackTimer / currentAttackDuration;

                // Check combo window
                _inComboWindow = normalizedTime >= _config.comboWindowStart / currentAttackDuration
                                 && normalizedTime <= _config.comboWindowEnd / currentAttackDuration;

                // Attack finished
                if (_attackTimer >= currentAttackDuration)
                {
                    // Check if next attack was queued
                    if (_nextAttackQueued && _currentAttackIndex < 2) // Max 3 attacks (0, 1, 2)
                    {
                        // Continue combo
                        StartAttack(_currentAttackIndex + 1);
                    }
                    else
                    {
                        // End attack sequence
                        EndAttack();
                    }
                }
            }
        }

        public bool IsAttacking()
        {
            return _isAttacking;
        }

        public int GetCurrentAttackIndex()
        {
            return _currentAttackIndex;
        }

        public bool CanMove()
        {
            if (!_isAttacking) return true;
            return !_config.blockMovementDuringAttack;
        }

        public bool CanRotate()
        {
            if (!_isAttacking) return true;
            return !_config.blockRotationDuringAttack;
        }

        private void StartAttack(int attackIndex)
        {
            _isAttacking = true;
            _currentAttackIndex = attackIndex;
            _attackTimer = 0f;
            _nextAttackQueued = false;
            _inComboWindow = false;

            Debug.Log($"[AttackSystem] Started Attack{attackIndex + 1}");
        }

        private void EndAttack()
        {
            _isAttacking = false;
            _currentAttackIndex = -1;
            _attackTimer = 0f;
            _cooldownTimer = _config.attackCooldown;
            _nextAttackQueued = false;
            _inComboWindow = false;

            Debug.Log("[AttackSystem] Attack sequence ended");
        }

        private float GetCurrentAttackDuration()
        {
            switch (_currentAttackIndex)
            {
                case 0: return _config.attack1Duration;
                case 1: return _config.attack2Duration;
                case 2: return _config.attack3Duration;
                default: return _config.attack1Duration;
            }
        }
    }
}
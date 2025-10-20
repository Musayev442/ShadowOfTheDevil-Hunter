using System;
using UnityEngine;
using App.Code.Character.Stats;
using App.Code.Core.FSM;
using App.Code.Core.Systems.Interfaces;
using App.Code.Core.Systems.Movement;
using App.Code.Core.Systems.HealthSystem;
using App.Code.Core.Systems.Movement.Interfaces;
using App.Code.Core.Systems.StaminaSystem;

namespace App.Code.Character.Base
{
    public abstract class SoulCharacter : MonoBehaviour
    {
        //[Header("Character Stats")] [SerializeField]
        //protected CharacterStats stats;
        
        // State Machine
        protected StateMachine stateMachine;

        [Header("Components")] [SerializeField]
        protected Rigidbody rb;

        [SerializeField] protected Animator animator;
        [SerializeField] protected CharacterStats baseStats;

        [Header("System")] protected IHealthSystem healthSystem;
        protected IStaminaSystem staminaSystem;
        protected IManaSystem manaSystem;
        protected IMovementSystem movementSystem;
        protected ICombatSystem combatSystem;


        // Properties
        //public CharacterStats Stats => baseStats;
        public IHealthSystem Health => healthSystem;
        public IStaminaSystem Stamina => staminaSystem;
        public IManaSystem Mana => manaSystem;
        public IMovementSystem Movement => movementSystem;
        public ICombatSystem Combat => combatSystem;

        /// <summary>
        /// Initialize character components and systems
        /// </summary>
        protected virtual void Awake()
        {
        }

        /// <summary>
        /// Initialize all character systems
        /// </summary>
        /// <summary>
        /// Handle character death
        /// </summary>
        protected virtual void HandleDeath()
        {
            // Override in derived classes for specific death behavior
            Debug.Log($"{gameObject.name} has died!");
        }

        protected virtual void OnDestroy()
        {
            if (healthSystem != null)
                healthSystem.OnDeath -= HandleDeath;
        }
    }
}
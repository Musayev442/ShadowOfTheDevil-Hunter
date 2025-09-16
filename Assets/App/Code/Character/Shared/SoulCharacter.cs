using Assets.App.Code.Animation.Interfaces;
using Assets.App.Code.Character.System;
using Assets.App.Code.Scripts;
using Assets.App.Code.Scripts.Core.Interfaces;
using Assets.App.Code.Scripts.Data;
using System;
using System.Collections;
using UnityEngine;

namespace SotD.Characters
{
    public abstract class SoulCharacter : MonoBehaviour, IDamageable, IStaminaUser
    {
        [Header("Modules")]
        [SerializeField] public Animator animator;
        [SerializeField] public Rigidbody rb;
        [SerializeField] protected float _acceleration = 10f;

        // Systems
        public HealthSystem HealthSystem { get; protected set; }
        public StaminaSystem StaminaSystem { get; protected set; }
        public bool IsAlive => HealthSystem.IsAlive;

        // Combat
        public float Poise = 10f;
        public float CurrentPoise { get; protected set; }
        public bool IsInvulnerable = false;
        public bool IsStaggered = false;

        public IMovable movement;
        protected ISprintAnimation sprintAnimation;
        protected IFreeMovementAnimation freeMovementAnimation;
        public CharacterStats stats = new CharacterStats();


        private void Awake()
        {
            movement = new Movement(rb, _acceleration);
            freeMovementAnimation = new CharacterAnimation(animator);
        }
        private void Start()
        {

            HealthSystem = new HealthSystem(stats.Vitality * 10);
            StaminaSystem = new StaminaSystem(stats.Endurance * 10f);
            CurrentPoise = Poise;

            HealthSystem.OnDeath += Die;
            HealthSystem.OnHealthChanged += HandleHealthChanged;
            HealthSystem.OnDeath += HandleDeath;
        }

        protected virtual void Update()
        {
            if (IsAlive)
            {
                StaminaSystem.RegenerateStamina(Time.deltaTime);
            }
        }


        private void HandleDeath()
        {
            Debug.Log("Player died!");
            // Play death animation, reload scene, etc.
        }

        private void HandleHealthChanged(float current, float max)
        {
            Debug.Log($"Health: {current}/{max}");
            // Update UI bar here
        }

        public virtual void TakeDamage(DamageData damageData)
        {
            if (IsInvulnerable || !IsAlive) return;

            int calculatedDamage = CalculateDamageTaken(damageData);
            HealthSystem.TakeDamage(calculatedDamage);

            HandlePoiseDamage(damageData);
        }

        protected virtual int CalculateDamageTaken(DamageData damageData)
        {
            float resistance = GetDamageResistance(damageData.DamageType);
            return Mathf.RoundToInt(damageData.BaseDamage * (1 - resistance));
        }

        protected virtual float GetDamageResistance(DamageType type)
        {
            return type switch
            {
                DamageType.Physical => 0.1f,
                DamageType.Magic => 0.05f,
                DamageType.Fire => 0.08f,
                DamageType.Lightning => 0.03f,
                DamageType.Dark => 0.12f,
                _ => 0f
            };
        }

        protected virtual void HandlePoiseDamage(DamageData damageData)
        {
            CurrentPoise -= damageData.BaseDamage * 0.5f;
            if (CurrentPoise <= 0)
            {
                OnPoiseBreak();
            }
        }

        protected virtual void OnPoiseBreak()
        {
            IsStaggered = true;
            CurrentPoise = Poise;
            StartCoroutine(ResetStagger());
        }

        protected IEnumerator ResetStagger()
        {
            yield return new WaitForSeconds(2f);
            IsStaggered = false;
        }

        public virtual void Die()
        {
            Debug.Log($"{name} has died");
        }

        public bool ConsumeStamina(float amount)
        {
            return StaminaSystem.ConsumeStamina(amount);
        }

        protected virtual void OnDestroy()
        {
            if (HealthSystem != null)
            {
                HealthSystem.OnDeath -= Die;
            }
        }
    }
}

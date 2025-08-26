using Assets.App.Code.Animation.Interfaces;
using Assets.App.Code.Character.System;
using UnityEngine;

namespace SotD.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [Header("Modules")]
        [SerializeField] public Animator animator;
        [SerializeField] public Rigidbody rb;
        [SerializeField] protected float _acceleration = 10f;

        public IMovable movement;

        protected HealthSystem health;
        protected ISprintAnimation sprintAnimation;
        protected IFreeMovementAnimation freeMovementAnimation;

        private int maxHealth = 100;

        private void Awake()
        {
            movement = new Movement(rb, _acceleration);
            freeMovementAnimation = new CharacterAnimation(animator);
        }
        private void Start()
        {
            health = new HealthSystem(maxHealth); // max health = 100
            //health.OnHealthChanged += HandleHealthChanged;
            //health.OnDeath += HandleDeath;
        }
    }
}

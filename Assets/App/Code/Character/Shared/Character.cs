using Assets.App.Code.Animation.Interfaces;
using Assets.App.Code.Character.System;
using UnityEngine;

namespace SotD.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [Header("Modules")]
        [SerializeField] protected Animator animator;
        [SerializeField] protected new Rigidbody characterRigidbody;

        public IFreeMovementAnimation freeMovementAnimation;

        public float speed = 5f;


        protected HealthSystem health;
        protected IPlayerInput playerInput;
        protected IMovable movement;
        protected ISprintAnimation sprintAnimation;

        private int maxHealth = 100;

        private void Awake()
        {
            playerInput = new Input_PC();
            movement = new Movement(characterRigidbody, speed);
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

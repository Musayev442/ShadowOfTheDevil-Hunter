using UnityEngine;

namespace SotD.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [Header("Modules")]
        [SerializeField] protected Animator animator;
        [SerializeField] protected new Rigidbody character_rb;

        public float speed = 5f;

        protected IPlayerInput playerInput;
        protected IMovable movement;
        protected ICharacterAnimation characterAnimation;

        private void Awake()
        {
            playerInput = new Input_PC();
            movement = new Movement(character_rb);
            characterAnimation = new CharacterAnimation(animator);
        }
    }
}

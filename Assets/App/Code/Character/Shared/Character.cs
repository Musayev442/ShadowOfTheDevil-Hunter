using UnityEngine;
using SotD.Characters.Shared;            // your interfaces live here (IHealth, ICombat, etc.)

namespace SotD.Characters
{
    /// <summary>Abstract base for all characters (Player, Enemy, NPC).</summary>
    public abstract class Character : MonoBehaviour
    {
        [Header("Modules")]
        [SerializeField] protected Animator animator;

        // Shared modules (plug concrete implementations via components)
        //protected IHealth health;
        //protected ICombat combat;
        //protected IPerception perception;
        //protected IInteractor interactor;

        // Shared data/stats container (ScriptableObject or runtime)
        //[SerializeField] protected CharacterStats stats;

        /*protected virtual void Awake()
        {
            health     = GetComponent<IHealth>();
            combat     = GetComponent<ICombat>();
            perception = GetComponent<IPerception>();
            interactor = GetComponent<IInteractor>();
        }

        protected virtual void Start() { }
        protected virtual void Update() { }

        public virtual void Initialize() { }
        public virtual void TakeDamage(DamageInfo dmg) { }
        public virtual void Die() { }
    }*/
}

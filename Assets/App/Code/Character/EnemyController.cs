using UnityEngine;

namespace SotD.Characters.Enemy
{
    /// <summary>Drives the Enemy (AI updates, pathing, state changes).</summary>
    [RequireComponent(typeof(Enemy))]
    public class EnemyController : MonoBehaviour
    {
        private Enemy enemy;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        private void Start()
        {
            //enemy.Initialize();
        }

        private void Update()
        {
            // Run simple AI checks or forward to a state machine/BT when added
        }
    }
}

using UnityEngine;

namespace SotD.Characters.Player
{
    /// <summary>Drives the Player (input, state updates, animation hooks).</summary>
    [RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour
    {
        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Start()
        {
            //player.Initialize();
        }

        private void Update()
        {
            // Read input, tick simple logic, or forward to a state machine (when added)
        }
    }
}

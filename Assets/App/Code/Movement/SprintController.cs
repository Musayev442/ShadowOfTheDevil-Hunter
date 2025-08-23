using App.Code.Movement.Interfaces;
using Assets.App.Code.Character.System.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Movement
{
    public class SprintController : ISprintable
    {
        private readonly IStamina stamina;
        private readonly Rigidbody rb;
        private readonly float sprintSpeed;
        private readonly float drainRate;

        public SprintController(IStamina stamina, Rigidbody rb, float sprintSpeed, float drainRate)
        {
            this.stamina = stamina;
            this.rb = rb;
            this.sprintSpeed = sprintSpeed;
            this.drainRate = drainRate;
        }

        public bool CanSprint => stamina.HasStamina(drainRate * Time.deltaTime);

        public void Sprint(Vector3 direction)
        {
            if (!CanSprint) return;

            rb.linearVelocity = direction.normalized * sprintSpeed;
            stamina.Consume(drainRate * Time.deltaTime);
        }
    }
}
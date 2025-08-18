using System.Collections;
using UnityEngine;

namespace App.Code.Movement.Interfaces
{
    public interface IFlyer
    {
        void Fly(Vector3 direction, float altitude);
    }
}
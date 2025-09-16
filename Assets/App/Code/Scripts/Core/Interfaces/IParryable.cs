using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Scripts.Core.Interfaces
{
    public interface IParryable
    {
        bool CanParry { get; }
        void OnParried();
    }
}
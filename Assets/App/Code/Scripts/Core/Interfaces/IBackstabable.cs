using Assets.App.Code.Scripts.Data;
using System.Collections;
using UnityEngine;

namespace Assets.App.Code.Scripts.Core.Interfaces
{
    public interface IBackstabable
    {
        void OnBackstabbed(DamageData damageData);
    }
}
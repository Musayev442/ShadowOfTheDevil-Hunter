using UnityEngine;
using UnityEngine.UI;

namespace App.Code.UI.HUD.Views
{
    public class HealthBarView
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private readonly float _smoothSpeed = 10f;
        
        private float _targetFill;
        private float _currentFill = 1f;
        
        private void Update()
        {
            _currentFill = Mathf.Lerp(_currentFill, _targetFill, Time.deltaTime * _smoothSpeed);
            _fillImage.fillAmount = _currentFill;
        }
        
        public void UpdateFill(float fillAmount)
        {
            _targetFill = Mathf.Clamp01(fillAmount);
        }
        
        public void SetFillImmediate(float fillAmount)
        {
            _targetFill = Mathf.Clamp01(fillAmount);
            _currentFill = _targetFill;
            _fillImage.fillAmount = _targetFill;
        }
    }
}
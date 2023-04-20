using TMPro;
using UnityEngine;

namespace Assets.Source.CodeBase.Ui
{
    public class BuyZoneInvestDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _investRemainingText;
        [SerializeField] private BuyZone _buyZone;
        [SerializeField] private Canvas _canvas;

        private void Awake() => 
            _canvas.worldCamera = Camera.main;

        private void OnEnable() => 
            _buyZone.InvestedCountChanged += OnInvestCountChanged;

        private void OnDisable() => 
            _buyZone.InvestedCountChanged -= OnInvestCountChanged;

        private void OnInvestCountChanged() => 
            _investRemainingText.text = _buyZone.RemainingInvest.ToString();
    }
}

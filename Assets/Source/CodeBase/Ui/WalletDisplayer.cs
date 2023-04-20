using Assets.Source.CodeBase.MainCharacter;
using TMPro;
using UnityEngine;

namespace Assets.Source.CodeBase.Ui
{
    public class WalletDisplayer : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private TMP_Text _moneyCountText;

        private void Awake() => 
            _moneyCountText.text = _wallet.Money.ToString();

        private void OnEnable() => 
            _wallet.MoneyChanged += OnMoneyChanged;

        private void OnDisable() => 
            _wallet.MoneyChanged += OnMoneyChanged;

        private void OnMoneyChanged(int moneyCount) => 
            _moneyCountText.text = moneyCount.ToString();
    }
}

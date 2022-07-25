using System.Collections.Generic;

namespace UnrulableWallet.UI.Models
{
    public class WalletModel
    {
        private string _currentOpenedWallet;
        private List<string> _availableWallets;

        public List<string> AvailableWallets
        {
            get
            {
                if (_availableWallets == null)
                {
                    return new List<string>();
                }
                return _availableWallets;
            }
            set
            {
                _availableWallets = value;
            }
        }

        public string CurrentOpenedWalletName
        {

            get
            {
                if (_currentOpenedWallet == null)
                {
                    return string.Empty;
                }
                return _currentOpenedWallet;
            }
            set
            {
                _currentOpenedWallet = value;
            }
        }
    }
}

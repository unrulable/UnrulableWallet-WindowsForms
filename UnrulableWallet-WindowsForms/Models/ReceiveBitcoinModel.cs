using System.Collections.Generic;
using System.Linq;
using NBitcoin;
using QBitNinja.Client.Models;

namespace UnrulableWallet.UI.Models
{
    public class ReceiveBitcoinModel
    {
        private string _bitcoinAddress;
        private List<BitcoinAddress> _bitcoinAddresses;

        public ReceiveBitcoinModel(BitcoinAddress retrievedAddressInfo, Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerReceiveAddresses)
        {
            List<BitcoinAddress> bitcoinAddressKeysList = operationsPerReceiveAddresses.Keys.ToList();
            _bitcoinAddress = retrievedAddressInfo.ToString();
            _bitcoinAddresses = new List<BitcoinAddress>();
            _bitcoinAddresses.AddRange(bitcoinAddressKeysList);
        }

        public string BitcoinAddress
        {
            get
            {
                return _bitcoinAddress;
            }
            set
            {
                _bitcoinAddress = value;
            }
        }


        public List<BitcoinAddress> AllAvailableBitcoinAddresses
        {
            get
            {
                return _bitcoinAddresses;
            }
            set
            {
                _bitcoinAddresses = value;
            }
        }
    }
}

using System.Collections.Generic;
using NBitcoin;

namespace UnrulableWallet.UI
{

    public static class FakeData
    {
        public static class FakeSafe
        {
            private static List<BitcoinAddress> fakeSafeAddresses = new List<BitcoinAddress>();
            static FakeSafe()
            {
                var addresses = new List<string>
                {
                    "1CCXoPdEpWr2KZ1xJz5QHyUR3Nn8yFeCut"
                };
                HashSet<string> uniqueAddresses = new HashSet<string>();
                foreach (var addr in addresses)
                    uniqueAddresses.Add(addr);
                foreach (var addr in uniqueAddresses)
                    fakeSafeAddresses.Add(BitcoinAddress.Create(addr, Network.Main));
            }
            public static Network Network => Network.Main;
            public static BitcoinAddress GetAddress(int index)
            {
                return index < fakeSafeAddresses.Count ? fakeSafeAddresses[index] : null;
            }
            public static List<BitcoinAddress> GetFirstNAddresses(int addressCount)
            {
                var addresses = new List<BitcoinAddress>();
                for (var i = 0; i < addressCount; i++)
                {
                    addresses.Add(GetAddress(i));
                }
                return addresses;
            }
        }
    }
}

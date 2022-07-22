using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System.Collections.Generic;
using System.Linq;
using HBitcoin.KeyManagement;
using static UnrulableWallet.UI.Shared.Enums;
using UnrulableWallet.UI.Models;

namespace UnrulableWallet.UI.Wrapper
{
	public static class QBitNinjaWrapper
	{
        public static void GetBalances(IEnumerable<AddressHistoryRecord> addressHistoryRecords, out Money confirmedBalance, out Money unconfirmedBalance)
        {
            confirmedBalance = Money.Zero;
            unconfirmedBalance = Money.Zero;
            foreach (var record in addressHistoryRecords)
            {
                if (record.Confirmed)
                    confirmedBalance += record.Amount;
                else
                {
                    unconfirmedBalance += record.Amount;
                }
            }
        }

        //public static bool SelectCoins(ref HashSet<Coin> coinsToSpend, Money totalOutAmount, List<Coin> unspentCoins)
        //{
        //	var haveEnough = false;
        //	foreach (var coin in unspentCoins.OrderByDescending(x => x.Amount))
        //	{
        //		coinsToSpend.Add(coin);
        //		// if doesn't reach amount, continue adding next coin
        //		if (coinsToSpend.Sum(x => x.Amount) < totalOutAmount) continue;
        //		else
        //		{
        //			haveEnough = true;
        //			break;
        //		}
        //	}

        //	return haveEnough;
        //}
        //public static Dictionary<Coin, bool> GetUnspentCoins(IEnumerable<ISecret> secrets)
        //{
        //	var unspentCoins = new Dictionary<Coin, bool>();
        //	foreach (var secret in secrets)
        //	{
        //		var destination = secret.PrivateKey.GetScriptPubKey(ScriptPubKeyType.Legacy).GetDestinationAddress(Config.Network);

        //		var client = new QBitNinjaClient(Config.Network);
        //		var balanceModel = client.GetBalance(destination, unspentOnly: true).Result;
        //		foreach (var operation in balanceModel.Operations)
        //		{
        //			foreach (var elem in operation.ReceivedCoins.Select(coin => coin as Coin))
        //			{
        //				unspentCoins.Add(elem, operation.Confirmations > 0);
        //			}
        //		}
        //	}

        //	return unspentCoins;
        //}
        //public static Dictionary<uint256, List<BalanceOperation>> GetOperationsPerTransactions(Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerAddresses)
        //{
        //	// 1. Get all the unique operations
        //	var opSet = new HashSet<BalanceOperation>();
        //	foreach (var elem in operationsPerAddresses)
        //		foreach (var op in elem.Value)
        //			opSet.Add(op);
        //	if (opSet.Count() == 0) Program.Exit("Wallet has no history yet.");

        //	// 2. Get all operations, grouped by transactions
        //	var operationsPerTransactions = new Dictionary<uint256, List<BalanceOperation>>();
        //	foreach (var op in opSet)
        //	{
        //		var txId = op.TransactionId;
        //		List<BalanceOperation> ol;
        //		if (operationsPerTransactions.TryGetValue(txId, out ol))
        //		{
        //			ol.Add(op);
        //			operationsPerTransactions[txId] = ol;
        //		}
        //		else operationsPerTransactions.Add(txId, new List<BalanceOperation> { op });
        //	}

        //	return operationsPerTransactions;
        //}
        public static Dictionary<BitcoinAddress, List<BalanceOperation>> QueryOperationsPerSafeAddresses(Safe safe, int minUnusedKeys = 7, HdPathType? hdPathType = null)
        {
            if (hdPathType == null)
            {
                Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerReceiveAddresses = QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Receive);
                Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerChangeAddresses = QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Change);

                var operationsPerAllAddresses = new Dictionary<BitcoinAddress, List<BalanceOperation>>();
                foreach (var elem in operationsPerReceiveAddresses)
                    operationsPerAllAddresses.Add(elem.Key, elem.Value);
                foreach (var elem in operationsPerChangeAddresses)
                    operationsPerAllAddresses.Add(elem.Key, elem.Value);
                return operationsPerAllAddresses;
            }

            var addresses = safe.GetFirstNAddresses(minUnusedKeys, hdPathType.GetValueOrDefault());
            //var addresses = FakeData.FakeSafe.GetFirstNAddresses(minUnusedKeys);

            var operationsPerAddresses = new Dictionary<BitcoinAddress, List<BalanceOperation>>();
            var unusedKeyCount = 0;
            foreach (var elem in QueryOperationsPerAddresses(addresses))
            {
                operationsPerAddresses.Add(elem.Key, elem.Value);
                if (elem.Value.Count == 0) unusedKeyCount++;
            }
            // WriteLine($"{operationsPerAddresses.Count} {hdPathType} keys are processed.");

            var startIndex = minUnusedKeys;
            while (unusedKeyCount < minUnusedKeys)
            {
                addresses = new List<BitcoinAddress>();
                for (int i = startIndex; i < startIndex + minUnusedKeys; i++)
                {
                    addresses.Add(safe.GetAddress(i, hdPathType.GetValueOrDefault()));
                    //addresses.Add(FakeData.FakeSafe.GetAddress(i));
                }
                foreach (var elem in QueryOperationsPerAddresses(addresses))
                {
                    operationsPerAddresses.Add(elem.Key, elem.Value);
                    if (elem.Value.Count == 0) unusedKeyCount++;
                }
                // WriteLine($"{operationsPerAddresses.Count} {hdPathType} keys are processed.");
                startIndex += minUnusedKeys;
            }

            return operationsPerAddresses;
        }
        public static Dictionary<BitcoinAddress, List<BalanceOperation>> QueryOperationsPerAddresses(IEnumerable<BitcoinAddress> addresses)
        {
            var operationsPerAddresses = new Dictionary<BitcoinAddress, List<BalanceOperation>>();
            var client = new QBitNinjaClient(Config.Network);
            foreach (var addr in addresses)
            {
                var operations = client.GetBalance(addr, unspentOnly: false).Result.Operations;
                operationsPerAddresses.Add(addr, operations);
            }
            return operationsPerAddresses;
        }

        /// <summary>
        /// Method to check wallet balance
        /// </summary>
        /// <param name="address"></param>
        /// <returns>Returns balance as decimal type</returns>
        public static decimal CheckBalance(BitcoinPubKeyAddress address)
		{
			QBitNinjaClient client = new QBitNinjaClient(Network.Main);
			var balanceModel = client.GetBalance(address, true).Result;
			decimal balance = 0;

			if (balanceModel.Operations.Count > 0)
			{
				var unspentCoins = new List<Coin>();
				foreach (var operation in balanceModel.Operations)
					unspentCoins.AddRange(operation.ReceivedCoins.Select(coin => coin as Coin));
				balance = unspentCoins.Sum(x => x.Amount.ToDecimal(MoneyUnit.BTC));
			}
			return balance;
		}

		/// <summary>
		/// Method to get list of recent blocks
		/// </summary>
		/// <param name="transactionIdToQuery"></param>
		/// <returns></returns>
		public static GetBlockResponse GetRecentBlocks(string transactionIdToQuery)
		{
			QBitNinjaClient client = new QBitNinjaClient(Network.Main);
			GetBlockResponse blockResponse = client.GetBlock(null).Result;
			return blockResponse;
		}

		/// <summary>
		/// Method to get transaction details by transaction id
		/// </summary>
		/// <param name="transactionIdToQuery"></param>
		/// <returns>Returns GetTransactionResponse object</returns>
		public static GetTransactionResponse QueryTransactionId(string transactionIdToQuery)
		{
			QBitNinjaClient client = new QBitNinjaClient(Network.Main);
			GetTransactionResponse transactionResponse = client.GetTransaction(uint256.Parse(transactionIdToQuery)).Result;
			return transactionResponse;
		}

        public static List<string> ShowBalances(string walletPath, Safe safe)
        {
            // 0. Query all operations, grouped by addresses
            Dictionary<BitcoinAddress, List<BalanceOperation>> operationsPerAddresses = QueryOperationsPerSafeAddresses(safe, 7);

            // 1. Get all address history record with a wrapper class
            var addressHistoryRecords = new List<AddressHistoryRecord>();
            foreach (var elem in operationsPerAddresses)
            {
                foreach (var op in elem.Value)
                {
                    addressHistoryRecords.Add(new AddressHistoryRecord(elem.Key, op));
                }
            }

            // 2. Calculate wallet balances
            Money confirmedWalletBalance;
            Money unconfirmedWalletBalance;
            GetBalances(addressHistoryRecords, out confirmedWalletBalance, out unconfirmedWalletBalance);

            // 3. Group all address history records by addresses
            var addressHistoryRecordsPerAddresses = new Dictionary<BitcoinAddress, HashSet<AddressHistoryRecord>>();
            foreach (var address in operationsPerAddresses.Keys)
            {
                var recs = new HashSet<AddressHistoryRecord>();
                foreach (var record in addressHistoryRecords)
                {
                    if (record.Address == address)
                        recs.Add(record);
                }
                addressHistoryRecordsPerAddresses.Add(address, recs);
            }

            // 4. Calculate address balances
            List<string> confirmedBalancesList = new List<string>();
            foreach (var elem in addressHistoryRecordsPerAddresses)
            {
                Money confirmedBalance;
                Money unconfirmedBalance;
                GetBalances(elem.Value, out confirmedBalance, out unconfirmedBalance);
                if (confirmedBalance != Money.Zero || unconfirmedBalance != Money.Zero)
                    confirmedBalancesList.Add($"{elem.Key}\t{confirmedBalance.ToDecimal(MoneyUnit.BTC).ToString("0.#############################")}\t\t{unconfirmedBalance.ToDecimal(MoneyUnit.BTC).ToString("0.#############################")}");
            };

            return confirmedBalancesList;
        }
	}
}

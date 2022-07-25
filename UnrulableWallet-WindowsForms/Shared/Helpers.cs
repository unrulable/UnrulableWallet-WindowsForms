using NBitcoin;
using System;
using System.Globalization;
using System.IO;

namespace UnrulableWallet.UI.Shared
{
    public static class Helpers
    {
        public static string GetArgumentValue(string[] args, string argName, bool required = true)
        {
            string argValue = "";
            foreach (var arg in args)
            {
                if (arg.StartsWith($"{argName}=", StringComparison.OrdinalIgnoreCase))
                {
                    argValue = arg.Substring(arg.IndexOf("=") + 1);
                    break;
                }
            }
            if (required && argValue == "")
            {
                throw new Exception($@"'{argName}=' is not specified.");
            }
            return argValue;
        }

        public static void AssertWalletNotExists(string walletFilePath)
        {
            if (File.Exists(walletFilePath))
            {
                throw new Exception($"A wallet, named {walletFilePath} already exists.");
            }
        }

        public static void AssertCorrectMnemonicFormat(string mnemonic)
        {
            try
            {
                if (new Mnemonic(mnemonic).IsValidChecksum)
                    return;
            }
            catch (FormatException) { }
            catch (NotSupportedException) { }

            throw new Exception("Incorrect mnemonic format.");
        }

        public static void AssertCorrectNetwork(Network network)
        {
            if (network != Config.Network)
            {
                throw new Exception($"The wallet you want to load is on the {network} Bitcoin network. But your config file specifies {Config.Network} Bitcoin network.");
            }
        }

        public static string GetWalletFilePath(string walletFileName)
        {
            //string walletFileName = GetArgumentValue(args, "wallet-file", required: false);
            if (walletFileName == "") walletFileName = Config.DefaultWalletFileName;

            var walletDirName = "Wallets";
            Directory.CreateDirectory(walletDirName);
            return Path.Combine(walletDirName, $"{walletFileName}.json");
        }

        public static Money ParseBtcString(string value)
        {
            decimal amount;
            if (!decimal.TryParse(
                    value.Replace(',', '.'),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out amount))
            {
                throw new Exception("Wrong btc amount format.");
            }


            return new Money(amount, MoneyUnit.BTC);
        }
    }
}

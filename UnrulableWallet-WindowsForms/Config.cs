﻿using NBitcoin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnrulableWallet.UI.Shared.Enums;

namespace UnrulableWallet.UI
{
    public static class Config
    {
        // Initialized with default attributes
        public static string DefaultWalletFileName = @"Wallet.json";
        public static Network Network = Network.Main;
        public static ConnectionType ConnectionType = ConnectionType.Http;

        static Config()
        {
            if (!File.Exists(ConfigFileSerializer.ConfigFilePath))
            {
                Save();
                Console.WriteLine($"{ConfigFileSerializer.ConfigFilePath} was missing. It has been created created with default settings.");
            }
            Load();
        }

        public static void Load()
        {
            var rawContent = ConfigFileSerializer.Deserialize();

            DefaultWalletFileName = rawContent.DefaultWalletFileName;

            if (rawContent.Network == Network.Main.ToString())
                Network = Network.Main;
            else if (rawContent.Network == Network.TestNet.ToString())
                Network = Network.TestNet;
            else if (rawContent.Network == null)
                throw new Exception($"Network is missing from {ConfigFileSerializer.ConfigFilePath}");
            else
                throw new Exception($"Wrong Network is specified in {ConfigFileSerializer.ConfigFilePath}");

            if (rawContent.ConnectionType == ConnectionType.FullNode.ToString())
                ConnectionType = ConnectionType.FullNode;
            else if (rawContent.ConnectionType == ConnectionType.Http.ToString())
                ConnectionType = ConnectionType.Http;
            else if (rawContent.ConnectionType == null)
                throw new Exception($"ConnectionType is missing from {ConfigFileSerializer.ConfigFilePath}");
            else
                throw new Exception($"Wrong ConnectionType is specified in {ConfigFileSerializer.ConfigFilePath}");
        }
        public static void Save()
        {
            ConfigFileSerializer.Serialize(DefaultWalletFileName, Network.ToString(), ConnectionType.ToString());
            Load();
        }
    }
    public class ConfigFileSerializer
    {
        public static string ConfigFilePath = "Config.json";
        // KEEP THEM PUBLIC OTHERWISE IT WILL NOT SERIALIZE!
        public string DefaultWalletFileName { get; set; }
        public string Network { get; set; }
        public string ConnectionType { get; set; }

        [JsonConstructor]
        private ConfigFileSerializer(string walletFileName, string network, string connectionType)
        {
            DefaultWalletFileName = walletFileName;
            Network = network;
            ConnectionType = connectionType;
        }

        internal static void Serialize(string walletFileName, string network, string connectionType)
        {
            var content =
                JsonConvert.SerializeObject(new ConfigFileSerializer(walletFileName, network, connectionType), Formatting.Indented);

            File.WriteAllText(ConfigFilePath, content);
        }

        internal static ConfigFileSerializer Deserialize()
        {
            if (!File.Exists(ConfigFilePath))
                throw new Exception($"Config file does not exist. Create {ConfigFilePath} before reading it.");

            var contentString = File.ReadAllText(ConfigFilePath);
            var configFileSerializer = JsonConvert.DeserializeObject<ConfigFileSerializer>(contentString);

            return new ConfigFileSerializer(configFileSerializer.DefaultWalletFileName, configFileSerializer.Network, configFileSerializer.ConnectionType);
        }
    }
}

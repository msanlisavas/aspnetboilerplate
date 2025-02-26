﻿using System.Configuration;
using Abp.Configuration.Startup;
using Abp.Extensions;

namespace Abp.Runtime.Caching.Redis
{
    public class AbpRedisCacheOptions
    {
        public IAbpStartupConfiguration AbpStartupConfiguration { get; }

        private const string ConnectionStringKey = "Abp.Redis.Cache";

        private const string DatabaseIdSettingKey = "Abp.Redis.Cache.DatabaseId";

        public string ConnectionString { get; set; }

        public int DatabaseId { get; set; }

        public string OnlineClientsStoreKey = "Abp.RealTime.OnlineClients";

        public string KeyPrefix { get; set; }

        public bool TenantKeyEnabled { get; set; }

        public AbpRedisCacheOptions(IAbpStartupConfiguration abpStartupConfiguration)
        {
            AbpStartupConfiguration = abpStartupConfiguration;

            ConnectionString = GetDefaultConnectionString();
            DatabaseId = GetDefaultDatabaseId();
            KeyPrefix = "";
            TenantKeyEnabled = false;
        }

        private static int GetDefaultDatabaseId()
        {
            var appSetting = ConfigurationManager.AppSettings[DatabaseIdSettingKey];
            if (appSetting.IsNullOrEmpty())
            {
                return -1;
            }

            int databaseId;
            if (!int.TryParse(appSetting, out databaseId))
            {
                return -1;
            }

            return databaseId;
        }

        private static string GetDefaultConnectionString()
        {
            var connStr = ConfigurationManager.ConnectionStrings[ConnectionStringKey];
            if (connStr == null || connStr.ConnectionString.IsNullOrWhiteSpace())
            {
                return "localhost";
            }

            return connStr.ConnectionString;
        }

        
    }
}
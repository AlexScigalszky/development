﻿using StackExchange.Redis;
using System;

namespace Example.Redis
{
    public class RedisBD
    {
        private static readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }

        static RedisBD()
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
               ConnectionMultiplexer.Connect("localhost:6379")
            );
        }
    }
}

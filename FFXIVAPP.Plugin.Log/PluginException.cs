// FFXIVAPP.Plugin.Log
// PluginException.cs
// 
// Copyright © 2013 ZAM Network LLC

using System;
using System.Runtime.Serialization;

namespace FFXIVAPP.Plugin.Log
{
    [Serializable]
    public class PluginException : Exception
    {
        public PluginException()
        {
        }

        public PluginException(string message) : base(message)
        {
        }

        public PluginException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PluginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

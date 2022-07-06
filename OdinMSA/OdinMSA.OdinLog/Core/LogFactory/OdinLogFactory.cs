﻿using System;
using System.Reflection;
using OdinMSA.OdinLog.Core.Models;
using OdinMSA.OdinPush.Core;

namespace OdinMSA.OdinLog.Core.LogFactory
{
    public class OdinLogFactory
    {
        public static IOdinLogFace? GetOdinLogUtils(EnumLogLevel logLevel, LogConfig config)
        {
            var ns = "OdinLog.Core.LogFactory.LogUtils";
            var classFullName = $"{ns}.OdinLog{logLevel.ToString()}";
            var clsName = Assembly.GetExecutingAssembly().GetType(classFullName);
            if (clsName != null)
                return Activator.CreateInstance(clsName, logLevel, config) as IOdinLogFace;
            else
                return null;
        }
    }
}


﻿// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Mobile.Unity;
using UnityEngine;

[Serializable]
public class MobileCenterSettings : ScriptableObject
{
    [AppSectet("iOS App Secret")]
    public string iOSAppSecret = "ios-app-secret";
    [AppSectet]
    public string AndroidAppSecret = "android-app-secret";
    [AppSectet]
    public string UWPAppSecret = "uwp-app-secret";
    
    [Tooltip("Mobile Center Analytics helps you understand user behavior and customer engagement to improve your app.")]
    public bool UseAnalytics = true;
    [Tooltip("Mobile Center Crashes will automatically generate a crash log every time your app crashes.")]
    public bool UseCrashes = true;
    [Tooltip("Mobile Center Distribute will let your users install a new version of the app when you distribute it via the Mobile Center.")]
    public bool UseDistribute = true;
    [Tooltip("Mobile Center Push enables you to send push notifications to users of your app from the Mobile Center portal.")]
    public bool UsePush = true;
    
    public LogLevel InitialLogLevel = LogLevel.Info;
    [Space]
    public LogUrlProperty CustomLogUrl = new LogUrlProperty();

    public string AppSecret
    {
        get
        {
            var appSecrets = new Dictionary<string, string>
            {
                { "ios", iOSAppSecret },
                { "android", AndroidAppSecret },
                { "uwp", UWPAppSecret }
            };
            return string.Concat(appSecrets
                .Where(i => !string.IsNullOrEmpty(i.Value))
                .Select(i => string.Format("{0}={1};", i.Key, i.Value))
                .ToArray());
        }
    }

    public Type[] Services
    {
        get
        {
            var services = new List<Type>();
            if (UseAnalytics)
            {
                services.Add(Analytics);
            }
            if (UseCrashes)
            {
                services.Add(Crashes);
            }
            if (UseDistribute)
            {
                services.Add(Distribute);
            }
            if (UsePush)
            {
                services.Add(Push);
            }
            return services.Where(i => i != null).ToArray();
        }
    }

    public static Type Analytics
    {
        get{ return Type.GetType("Microsoft.Azure.Mobile.Unity.Analytics.Analytics, Assembly-CSharp-firstpass"); }
    }

    public static Type Crashes
    {
        get { return Type.GetType("Microsoft.Azure.Mobile.Unity.Crashes.Crashes, Assembly-CSharp-firstpass"); }
    }

    public static Type Distribute
    {
        get { return Type.GetType("Microsoft.Azure.Mobile.Unity.Distribute.Distribute, Assembly-CSharp-firstpass"); }
    }

    public static Type Push
    {
        get { return Type.GetType("Microsoft.Azure.Mobile.Unity.Push.Push, Assembly-CSharp-firstpass"); }
    }
}
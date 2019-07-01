﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;



public static class SessionExtension
{

    public static void Set<T>(this ISession currentSession, string objKey, T objValue)
    {
        currentSession.SetString(objKey, JsonConvert.SerializeObject(objValue));
    }

    public static T Get<T>(this ISession currentSession, string objKey)
    {
        var objValue = currentSession.GetString(objKey);
        return objValue == null ? default(T) : JsonConvert.DeserializeObject<T>(objValue); 
    }


}


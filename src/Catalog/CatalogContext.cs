﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace NuGet.Services.Metadata.Catalog
{
    public class CatalogContext
    {
        ConcurrentDictionary<string, JObject> _jsonLdContext;

        public CatalogContext()
        {
            _jsonLdContext = new ConcurrentDictionary<string, JObject>();
        }

        public JObject GetJsonLdContext(string name, Uri type)
        {
            return _jsonLdContext.GetOrAdd(name + "#" + type.ToString(), (key) =>
            {
                using (JsonReader jsonReader = new JsonTextReader(new StreamReader(Utils.GetResourceStream(name))))
                {
                    JObject obj = JObject.Load(jsonReader);
                    obj["@type"] = type.ToString();
                    return obj;
                }
            });
        }
    }
}

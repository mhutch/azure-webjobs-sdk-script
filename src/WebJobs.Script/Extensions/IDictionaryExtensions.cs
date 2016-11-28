// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Microsoft.Azure.WebJobs.Script
{
    public static class IDictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IReadOnlyDictionary<TKey, TValue> other)
        {
            if (other == null)
            {
                return;
            }

            foreach (var pair in other)
            {
                dictionary[pair.Key] = pair.Value;
            }
        }

        public static IDictionary<string, TValue> AsCaseInsensitive<TValue>(this IDictionary<string, TValue> dictionary)
        {
            return new Dictionary<string, TValue>(dictionary, StringComparer.OrdinalIgnoreCase);
        }

        public static bool TryGetValue<TValue>(this IDictionary<string, object> obj, string name, out TValue value)
        {
            value = default(TValue);

            object tempValue = null;
            if (obj.TryGetValue(name, out tempValue))
            {
                if (tempValue is TValue)
                {
                    value = (TValue)tempValue;
                    return true;
                }
            }

            return false;
        }
    }
}

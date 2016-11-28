// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Xunit;

namespace Microsoft.Azure.WebJobs.Script.Tests.Extensions
{
    public class IDictionaryExtensionsTests
    {
        private Dictionary<string, object> _dictionary;

        public IDictionaryExtensionsTests()
        {
            _dictionary = new Dictionary<string, object>
            {
                { "BoolValue", true },
                { "IntValue", 777 },
                { "StringValue", "Mathew" }
            };
        }

        [Fact]
        public void TryGetValue_ReturnsExpectedResult()
        {
            bool boolResult;
            Assert.True(_dictionary.TryGetValue<bool>("BoolValue", out boolResult));
            Assert.Equal(_dictionary["BoolValue"], boolResult);

            string stringValue;
            Assert.False(_dictionary.TryGetValue<string>("BoolValue", out stringValue));
            Assert.Equal(null, stringValue);

            Assert.True(_dictionary.TryGetValue<string>("StringValue", out stringValue));
            Assert.Equal("Mathew", stringValue);

            Assert.False(_dictionary.TryGetValue<string>("DoesNotExist", out stringValue));
            Assert.Equal(null, stringValue);
        }

        [Fact]
        public void AsCaseInsensitive_ReturnsExpectedResult()
        {
            object value = null;
            Assert.True(_dictionary.TryGetValue("StringValue", out value));
            Assert.False(_dictionary.TryGetValue("stringvalue", out value));

            var caseInsensitiveDictionary = _dictionary.AsCaseInsensitive();

            Assert.True(caseInsensitiveDictionary.TryGetValue("StringValue", out value));
            Assert.Equal("Mathew", value);
            Assert.True(caseInsensitiveDictionary.TryGetValue("stringvalue", out value));
            Assert.Equal("Mathew", value);
        }
    }
}

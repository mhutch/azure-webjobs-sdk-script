// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Azure.WebJobs.Script.Binding;
using Xunit;

namespace Microsoft.Azure.WebJobs.Script.Tests.Binding
{
    public class HttpBindingTests
    {
        [Fact]
        public void AddResponseHeader_ContentMD5_AddsExpectedHeader()
        {
            HttpResponseMessage response = new HttpResponseMessage()
            {
                Content = new StringContent("Test")
            };
            byte[] bytes = Encoding.UTF8.GetBytes("This is a test");
            var header = new KeyValuePair<string, object>("content-md5", bytes);
            HttpBinding.AddResponseHeader(response, header);
            Assert.Equal(bytes, response.Content.Headers.ContentMD5);

            response = new HttpResponseMessage()
            {
                Content = new StringContent("Test")
            };
            string base64 = Convert.ToBase64String(bytes);
            header = new KeyValuePair<string, object>("content-md5", base64);
            HttpBinding.AddResponseHeader(response, header);
            Assert.Equal(base64, Convert.ToBase64String(response.Content.Headers.ContentMD5));
        }
    }
}

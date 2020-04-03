using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using SeleniumCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCore.Tests.API
{

    [TestClass]
    public class GetCodeSystems
    {
        [TestMethod]
        public void GetCodeSystemsTest()
        {
            var restClient = new RestClient();
            var request = new RestRequest("CodeSystems", Method.GET);
            var response = restClient.Execute(request);
            response.StatusCode.ToString().Should().Be("OK");
            response.ResponseStatus.ToString().Should().Be("Completed");
            response.Content.Should().NotBeNullOrEmpty();
            var deserializedResponse = JsonConvert.DeserializeObject<List<Formats>>(response.Content);

            foreach (var item in deserializedResponse)
            {
                item.Id.Should().NotBeEmpty();
                item.Name.Should().NotBeEmpty();
                item.Language.Should().NotBeEmpty();
            }

        }
    }
}

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using SeleniumCore.Helpers;
using SeleniumCore.Models;

namespace SeleniumCore.Tests.API
{
    [TestClass]
    public class UploadSegments
    {
        [TestMethod]
        public void UploadSegmentsTest()
        {
            var containerID = "f8bbb31e-b01a-43f6-89bf-2d32abe95a4d";
            var filePath = EmbeddedResource.GetTestFileLocation("SegmentsAutomationFile.xml");

            var segmentsUploadResponse = GetFileUploadResponse(containerID, filePath);
            var deserializedResponse = JsonConvert.DeserializeObject<SegmentsResponseObject>(segmentsUploadResponse.Content); ;
            deserializedResponse.httpResponseMessage.StatusCode.ToString().Should().Be("201");
            deserializedResponse.httpResponseMessage.ReasonPhrase.ToString().Should().Be("Created");
            deserializedResponse.httpResponseMessage.IsSuccessStatusCode.Should().Be(true);

            foreach (var segment in deserializedResponse.sewer_pipes)
            {
                segment.key.Should().NotBeNull();
                segment.value.Should().NotBeNull();
            }

            deserializedResponse.format.Should().NotBeNull();
            deserializedResponse.codeSystem.Should().NotBeNull();
            deserializedResponse.language.Should().NotBeNull();
        }




        public IRestResponse GetFileUploadResponse(string containerId, string filePath)
        {
            var restClient = new RestClient();
            var request = new RestRequest("xmlfile", Method.POST);

            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddParameter("inputParameters", $@"{{ 'ContainerID' : '{containerId}' }}");
            request.AddFile("inputFile", filePath);

            var response = restClient.Execute(request);
            return response;
        }
    }
}


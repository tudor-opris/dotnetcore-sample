using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace SeleniumCore.Helpers
{
    public static class WriteToXML
    {
        public static string GetUpdatedXMLFileWithNewSegmentName(string segmentName)
        {
            var filePath = EmbeddedResource.GetTestFileLocation("SegmentsAutomationFile.xml");
            XmlDocument xmlDoc = new XmlDocument();
            string docFolderPath = EmbeddedResource.GetResourceFolderFilePath("SegmentsAutomationFile.xml");
            string newXmlDocPath = Path.Combine(docFolderPath, "SegmentsAutomationFile - " + Guid.NewGuid() + ".xml");
            xmlDoc.Load(filePath);
            xmlDoc.Save(newXmlDocPath);

            ReplaceValuesInXMLFile(newXmlDocPath, "Objektbezeichnung", segmentName);
            ReplaceValuesInXMLFile(newXmlDocPath, "Inspektionsdatum", DateTime.Today.ToString("yyyy-MM-dd"));
            ReplaceValuesInXMLFile(newXmlDocPath, "KnotenZulauf", segmentName);
            ReplaceValuesInXMLFile(newXmlDocPath, "KnotenAblauf", segmentName);

            return newXmlDocPath;
        }
        public static void ReplaceValuesInXMLFile(string filePath, string xmlFieldSelector, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            var nodes = root.GetElementsByTagName(xmlFieldSelector);

            foreach (XmlNode element in nodes)
            {
                if (element.FirstChild != null)
                {
                    element.FirstChild.Value = value;
                }
            }
            xmlDoc.Save(filePath);
        }
    }

  

}

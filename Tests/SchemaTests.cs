using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProductData.Tests
{
    [TestClass]
    public class SchemaTests
    {
        private const string SamplePath = @"XSD\Samples";
        private static readonly XmlSchemaSet SchemaSet;

        static SchemaTests()
        {
            SchemaSet = new XmlSchemaSet();
            SchemaSet.Add(null, AbsolutePathRelativeToEntryPointLocation(@"XSD\Schema.xsd"));
        }

        private static string AbsolutePathRelativeToEntryPointLocation(string relativePath)
        {
          var entryPoint = Assembly.GetAssembly(typeof(SchemaTests)) ;
          var basePath = Path.GetDirectoryName(entryPoint.Location);
          var combinedPath  = Path.Combine(basePath, relativePath);
          var canonicalPath = Path.GetFullPath(combinedPath);

          return canonicalPath ;
        }

        private static XDocument GetSample(string name)
        {
            var fullPath = AbsolutePathRelativeToEntryPointLocation(Path.Combine(SamplePath, name + ".xml"));
            return XDocument.Load(fullPath);
        }

        [TestMethod]
        public void TestSimpleCatalog()
        {
            TestDoc("SimpleCatalog");
        }

        private void TestDoc(string name)
        {
            var doc = GetSample(name);
            var msg = "";
            var valid = true;
            doc.Validate(SchemaSet, (sender, args) =>
            {
                msg += args.Message + Environment.NewLine;
                valid = false;
            });
            Assert.IsTrue(valid, msg);
        }
    }
}
//
//XmlSchemaSet schemas = new XmlSchemaSet();
//schemas.Add(schemaNamespace, schemaFileName);
//
//XDocument doc = XDocument.Load(filename);
//string msg = "";
//doc.Validate(schemas, (o, e) => {
//    msg += e.Message + Environment.NewLine;
//});
//Console.WriteLine(msg == "" ? "Document is valid" : "Document invalid: " + msg);
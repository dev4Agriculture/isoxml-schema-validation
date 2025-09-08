using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

class XPathValidation
{
    private static Dictionary<string,XmlReaderSettings> schemaSettings = new Dictionary<string, XmlReaderSettings>();
    private static int _validationErrorCount = 0;
    private static XmlReaderSettings CreateSettings(string key, string xsdRootPath, params string[] schemaFiles)
    {
        var settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
                            | XmlSchemaValidationFlags.ProcessIdentityConstraints
                            | XmlSchemaValidationFlags.ProcessInlineSchema
                            | XmlSchemaValidationFlags.ProcessSchemaLocation
        };

        foreach (var schemaFile in schemaFiles)
        {
            settings.Schemas.Add("", Path.Combine(xsdRootPath, schemaFile));
        }

        return settings;
    }

    public static void  Init(string xsdRootPath)
    {
        // -----------------------------TASKDATA-----------------------------
        schemaSettings.Add("4.4_TaskData", CreateSettings("4.4_TaskData", xsdRootPath, "ISO11783_TaskFile_V4-4.xsd", "ISO11783_Common_V4-4.xsd"));
        schemaSettings.Add("4.3_TaskData", CreateSettings("4.3_TaskData", xsdRootPath, "ISO11783_TaskFile_V4-3.xsd", "ISO11783_Common_V4-3.xsd"));
        schemaSettings.Add("4.2_TaskData", CreateSettings("4.2_TaskData", xsdRootPath, "ISO11783_TaskFile_V4-2.xsd", "ISO11783_Common_V4-2.xsd"));
        schemaSettings.Add("4.0_TaskData", CreateSettings("4.0_TaskData", xsdRootPath, "ISO11783_TaskFile_V4-0.xsd"));
        schemaSettings.Add("3.3_TaskData", CreateSettings("3.3_TaskData", xsdRootPath, "ISO11783_TaskFile_V3-3.xsd"));
        schemaSettings.Add("3.2_TaskData", CreateSettings("3.2_TaskData", xsdRootPath, "ISO11783_TaskFile_V3-2.xsd"));
        schemaSettings.Add("3.1_TaskData", CreateSettings("3.1_TaskData", xsdRootPath, "ISO11783_TaskFile_V3-1.xsd"));
        schemaSettings.Add("3.0_TaskData", CreateSettings("3.0_TaskData", xsdRootPath, "ISO11783_TaskFile_V3-0.xsd"));
        schemaSettings.Add("2.1_TaskData", CreateSettings("2.1_TaskData", xsdRootPath, "ISO11783_TaskFile_V2-1.xsd"));
        schemaSettings.Add("2.0_TaskData", CreateSettings("2.0_TaskData", xsdRootPath, "ISO11783_TaskFile_V2-0.xsd"));

        // -----------------------------LINKLIST-----------------------------
        schemaSettings.Add("4.2_LinkList", CreateSettings("4.2_LinkList", xsdRootPath, "ISO11783_LinkListFile_V4-2.xsd"));
        schemaSettings.Add("4.3_LinkList", CreateSettings("4.3_LinkList", xsdRootPath, "ISO11783_LinkListFile_V4-3.xsd"));
        schemaSettings.Add("4.4_LinkList", CreateSettings("4.4_LinkList", xsdRootPath, "ISO11783_LinkListFile_V4-4.xsd"));

        // -----------------------------EXTERNAL FILE-----------------------------
        schemaSettings.Add("2.0_ExternalFile", CreateSettings("2.0_ExternalFile", xsdRootPath, "ISO11783_ExternalFile_V2-0.xsd"));
        schemaSettings.Add("3.0_ExternalFile", CreateSettings("3.0_ExternalFile", xsdRootPath, "ISO11783_ExternalFile_V3-0.xsd"));
        schemaSettings.Add("4.0_ExternalFile", CreateSettings("4.0_ExternalFile", xsdRootPath, "ISO11783_ExternalFile_V4-0.xsd"));
        schemaSettings.Add("4.2_ExternalFile", CreateSettings("4.2_ExternalFile", xsdRootPath, "ISO11783_ExternalFile_V4-2.xsd", "ISO11783_Common_V4-2.xsd"));
        schemaSettings.Add("4.3_ExternalFile", CreateSettings("4.3_ExternalFile", xsdRootPath, "ISO11783_ExternalFile_V4-3.xsd", "ISO11783_Common_V4-3.xsd"));
        schemaSettings.Add("4.4_ExternalFile", CreateSettings("4.4_ExternalFile", xsdRootPath, "ISO11783_ExternalFile_V4-4.xsd", "ISO11783_Common_V4-4.xsd"));

        // -----------------------------TIMELOG-----------------------------
        schemaSettings.Add("4.3_TimeLogFile", CreateSettings("4.3_TimeLogFile", xsdRootPath, "ISO11783_TimeLog_V4-3.xsd"));
        schemaSettings.Add("4.4_TimeLogFile", CreateSettings("4.4_TimeLogFile", xsdRootPath, "ISO11783_TimeLog_V4-4.xsd"));
    }

    private static string rootPath = "C:\\src\\AEF\\ISOBUS-XML-schema\\test";

    static string getSchemaDefinitionName(XmlDocument document)
    {
        var rootNode = document.FirstChild;
        while (rootNode != null && rootNode.NodeType != XmlNodeType.Element)
        {
            rootNode = rootNode.NextSibling;
        }
        if (rootNode == null)
        {
            return "";
        }
        
        if (rootNode.Name.Equals("ISO11783_TaskData"))
        {
            return rootNode.Attributes["VersionMajor"].Value + "." + rootNode.Attributes["VersionMinor"].Value + "_TaskData";
        } else if (rootNode.Name.Equals("ISO11783LinkList"))
        {
            return rootNode.Attributes["VersionMajor"].Value + "." + rootNode.Attributes["VersionMinor"].Value + "_LinkList";
        } else if (rootNode.Name.Equals("TIM"))
        {
            return "4.4_TimeLogFile";//There is no versioning in TLGs
        } else if (rootNode.Name.Equals("XFC"))
        {
            return rootNode.Attributes["VersionMajor"].Value + "." + rootNode.Attributes["VersionMinor"].Value + "_ExternalFile";
        }

        return "";
    }

    static void Test(string filePath)
    {
        string resultIndicator = filePath.Substring(filePath.Length - "fails.xml".Length, "fails".Length);
        bool shallFail = false || resultIndicator.Equals("fails");
        try
        {
            Console.WriteLine("============================================");
            Console.WriteLine("Starting Test of File: " + filePath);
            //TODO: Reading the file twice is just not sexy :( 
            XmlReader reader = XmlReader.Create(Path.Combine(rootPath,filePath));
            XmlDocument document = new XmlDocument();
            document.Load(reader);

            var schemaName = getSchemaDefinitionName(document);
            XmlReaderSettings settings = null;
            if (schemaSettings.TryGetValue(schemaName, out settings))
            {
                reader = XmlReader.Create(Path.Combine(rootPath, filePath), settings);
                document = new XmlDocument();
                document.Load(reader);
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                document.Validate(eventHandler);
                if (shallFail) //If we reach this, something went wrong
                {
                    Console.WriteLine("=================================");
                    Console.WriteLine("===========ERROR!================");
                    Console.WriteLine("=================================");
                    Console.WriteLine("File "+filePath+"Should have failed but didn't");
                    _validationErrorCount++;
                }
                else
                {  
                    Console.WriteLine("Success: No Error found, no error expected");
                }
            }
            else
            {
                Console.WriteLine("Could not find corresponding Schema definition");
                _validationErrorCount++;
            }


        }
        catch (Exception ex)
        {
            if (!shallFail) //If we reach this, something went wrong
            {
                Console.WriteLine("=================================");
                Console.WriteLine("===========ERROR!================");
                Console.WriteLine("=================================");
                Console.WriteLine("File " + filePath + "Should not have failed but did!");
                Console.WriteLine(ex.Message);
                _validationErrorCount++;
            }
            else
            {
                Console.WriteLine("Success! Error: " + ex.Message);
            }
        }

    }

    static int Main()
    {
        string xsdRootPath = "D:\\src\\AEF\\ISOBUS-XML-schema";
        Console.WriteLine($"Select ISOXML Schema Path: (Default: {xsdRootPath})");
        var exchangeFolder = Console.ReadLine();
        if (!string.IsNullOrEmpty(exchangeFolder))
        {
            xsdRootPath = exchangeFolder;
        }

        string testFolder = Path.Join(xsdRootPath,"test");

        Init(xsdRootPath);
        foreach (var file in Directory.GetFiles(testFolder))
        {
            if (file.EndsWith(".xml"))
            {
                Test(Path.Combine(testFolder,file));
            }

        }

        if (_validationErrorCount == 0)
        {
            Console.WriteLine("##################################################");
            Console.WriteLine("             SUCCESS");
            Console.WriteLine("##################################################");
        }
        else
        {
            Console.WriteLine("##################################################");
            Console.WriteLine("             ERROR");
            Console.WriteLine("##################################################");

        }

        return _validationErrorCount;
    }

    static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        switch (e.Severity)
        {
            case XmlSeverityType.Error:
                Console.WriteLine("Error: {0}", e.Message);
                break;
            case XmlSeverityType.Warning:
                Console.WriteLine("Warning {0}", e.Message);
                break;
        }
    }
}
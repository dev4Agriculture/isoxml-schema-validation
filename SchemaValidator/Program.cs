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
    static void Init(string xsdRootPath)
    {
        //-----------------------------TASKDATA-------------------------------------------------------------------------
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TaskFile_V4-4.xsd"));
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_Common_V4-4.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.4_TaskData", settings);

        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath,"ISO11783_TaskFile_V4-3.xsd"));
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_Common_V4-3.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.3_TaskData",settings);

        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TaskFile_V4-2.xsd"));
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_Common_V4-2.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.2_TaskData", settings);


        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TaskFile_V4-0.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.0_TaskData", settings);

        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TaskFile_V3-3.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("3.3_TaskData", settings);

        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TaskFile_V3-2.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("3.2_TaskData", settings);

        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TaskFile_V3-1.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("3.1_TaskData", settings);

        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TaskFile_V3-0.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("3.0_TaskData", settings);

        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TaskFile_V2-1.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("2.1_TaskData", settings);

        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TaskFile_V2-0.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("2.0_TaskData", settings);



        //-----------------------------LINKLIST-------------------------------------------------------------------------
        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_LinkListFile_V4-2.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.2_LinkList", settings);
        
        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_LinkListFile_V4-3.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.3_LinkList", settings);


        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_LinkListFile_V4-4.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.4_LinkList", settings);


        //-----------------------------EXTERNAL FILE-------------------------------------------------------------------------
        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_ExternalFile_V2-0.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("2.0_ExternalFile", settings);
        
        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_ExternalFile_V3-0.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("3.0_ExternalFile", settings);
        
        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_ExternalFile_V4-0.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.0_ExternalFile", settings);

        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_ExternalFile_V4-2.xsd"));
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_Common_V4-2.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.2_ExternalFile", settings);
        
        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_ExternalFile_V4-3.xsd"));
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_Common_V4-3.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.3_ExternalFile", settings);


        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_ExternalFile_V4-4.xsd"));
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_Common_V4-4.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.4_ExternalFile", settings);


        //-----------------------------TIMELOG-------------------------------------------------------------------------
        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TimeLog_V4-3.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.3_TimeLogFile", settings);


        settings = new XmlReaderSettings();
        settings.Schemas.Add("", Path.Combine(xsdRootPath, "ISO11783_TimeLog_V4-4.xsd"));
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationFlags =
            XmlSchemaValidationFlags.ReportValidationWarnings |
            XmlSchemaValidationFlags.ProcessIdentityConstraints |
            XmlSchemaValidationFlags.ProcessInlineSchema |
            XmlSchemaValidationFlags.ProcessSchemaLocation;
        schemaSettings.Add("4.4_TimeLogFile", settings);
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
        string testFolder = "C:\\src\\AEF\\ISOXML-Schema\\ISOBUS-XML-schema\\test";
        string xsdRootPath = "C:\\src\\AEF\\ISOXML-Schema\\ISOBUS-XML-schema";

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
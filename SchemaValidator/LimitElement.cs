using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;
using System.Xml.XPath;

public class ElementLimitEntry
{
	public string elementName { get; set; }
	public int? minAmount { get; set; }
	public int? maxAmount { get; set; }


	public ElementLimitEntry()
	{


	}
}

public class ElementLimit
{
	public List<ElementLimitEntry> children { get; set; }
	public string rootName { get; set; }


}

public class ElementLimitList
{
	public List<ElementLimit> elements { get; set; }
	public int VersionMinor { get; set; }
	public int VersionMajor { get; set; }

	public static ElementLimitList FromJSONFile(String path)
	{
		return JsonSerializer.Deserialize<ElementLimitList>(File.ReadAllText(path));
	}

     private int CheckSubElementsOfSpecificOccurance(XmlNode xmlNode)
        {
        var amount = 0;
            if (!xmlNode.NodeType.Equals(XmlNodeType.Element))
            {
                return 0;
            }
            foreach (var limitElement in this.elements)
            {
                if (limitElement.rootName == xmlNode.Name)
                {
                    XPathNavigator navigator = xmlNode.CreateNavigator();
                    foreach (var limitEntry in limitElement.children)
                    {
                        XPathNodeIterator iterator = navigator.Select(limitEntry.elementName);
                        if (
                            (limitEntry.maxAmount != null && iterator.Count > limitEntry.maxAmount) ||
                            (limitEntry.minAmount != null && iterator.Count < limitEntry.minAmount)
                            )
                        {
                            var min = limitEntry.minAmount ?? 0;
                            var max = limitEntry.maxAmount ?? 1000_000_000;
                            OnXmlElementAmountMissmatch(limitEntry, xmlNode, iterator.Count);
                            break;
                        }
                    }
                    foreach (var child in xmlNode.ChildNodes)
                    {
                        amount += CheckSubElementsOfSpecificOccurance((XmlNode)child);
                    }
                }
            }

            return amount;
        }



        public int CheckXMLDocument(XmlDocument document)
        {
            var amount = 0;
            foreach (var childNode in document.ChildNodes)
            {
                amount +=    CheckSubElementsOfSpecificOccurance((XmlNode)childNode);

            }
        return amount;
        }


    public void OnXmlElementAmountMissmatch(ElementLimitEntry limitEntry, XmlNode xmlNode, int amount)
        {
            Console.WriteLine($"Amounts missmatch: {xmlNode.Name} should have between " +
                $"{limitEntry.minAmount ?? 0} and " +
                $"{limitEntry.maxAmount ?? 1000_000} entries of type {limitEntry.elementName} but has {amount} entries");
            throw new XMLElementAmountException();
        }
}
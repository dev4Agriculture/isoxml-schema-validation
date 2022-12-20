using System;
using System.Collections.Generic;

public class LimitElementEntry
{
	public string elementName { get; set; }
	public string parentElement { get; set; }
	public int minCount { get; set; }
	public int maxCount { get; set; }


	public LimitElement()
	{


	}
}

public class LimitElement
{
	public List<LimitElementEntry> entries = new List<LimitElementEntry>();
	public string parentElement;


}

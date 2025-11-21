using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions;

public class EnumControllerBase : ControllerBase
{
    protected readonly XDocument? _xmlComments;

    public EnumControllerBase()
    {
        // Load the XML documentation file for API documentation
        // Note: This is a placeholder implementation. Override in derived classes if needed.
        var xmlPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, 
            "WTE.TintTrack.Api.xml");
        
        if (File.Exists(xmlPath))
        {
            _xmlComments = XDocument.Load(xmlPath);
        }
    }
}
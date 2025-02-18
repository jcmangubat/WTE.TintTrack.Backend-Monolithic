using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions;

public class EnumControllerBase : ControllerBase
{
    protected readonly XDocument? _xmlComments;

    public EnumControllerBase()
    {
        // Load the XML documentation file (adjust the path as needed)
        var xmlPath = AppDomain.CurrentDomain.BaseDirectory + "YourAssemblyName.xml";
        if (System.IO.File.Exists(xmlPath))
            _xmlComments = XDocument.Load(xmlPath);
    }
}
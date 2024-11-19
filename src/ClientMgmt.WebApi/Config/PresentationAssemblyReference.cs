using System.Reflection;

namespace ClientMgmt.WebApi.Config;

public class PresentationAssemblyReference
{
    internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
}
using System.Reflection;

namespace AccountMgmt.WebApi.Config;

public class PresentationAssemblyReference
{
    internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
}
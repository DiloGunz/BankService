using System.Reflection;

namespace AccountMgmt.Application.Config;

public class ApplicationAssemblyReference
{
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}
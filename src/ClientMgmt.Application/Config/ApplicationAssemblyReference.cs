using System.Reflection;

namespace ClientMgmt.Application.Config;

/// <summary>
/// Clase que sirve como referencia centralizada al ensamblado de la capa de aplicaci�n.
/// Utilizada para prop�sitos como configuraci�n, registro de dependencias o descubrimiento de clases dentro de este ensamblado.
/// </summary>
public class ApplicationAssemblyReference
{
    /// <summary>
    /// Referencia al ensamblado actual.
    /// </summary>
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}

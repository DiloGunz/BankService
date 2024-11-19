using System.Reflection;

namespace ClientMgmt.Application.Config;

/// <summary>
/// Clase que sirve como referencia centralizada al ensamblado de la capa de aplicación.
/// Utilizada para propósitos como configuración, registro de dependencias o descubrimiento de clases dentro de este ensamblado.
/// </summary>
public class ApplicationAssemblyReference
{
    /// <summary>
    /// Referencia al ensamblado actual.
    /// </summary>
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}

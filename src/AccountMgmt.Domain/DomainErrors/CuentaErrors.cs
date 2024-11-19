using ErrorOr;

namespace AccountMgmt.Domain.DomainErrors;

public static class CuentaErrors
{
    public static Error NoEncontrado =>
        Error.NotFound("Cuenta.NotFound", "No se encontró la cuenta con el Id proporcionado.");

    public static Error SaldoInsuficiente =>
        Error.NotFound("Cuenta.SaldoInsuficiente", "No dispone de saldo suficiente.");

}
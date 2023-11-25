namespace bdd_obligatorio_api.Contracts.Login;

public record LoginResponse(
    string Username,
    string Password,
    string Token
);
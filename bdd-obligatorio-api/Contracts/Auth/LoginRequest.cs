namespace bdd_obligatorio_api.Contracts.Login;

public record LoginRequest(
    string Username,
    string Password
);
namespace EmbarcaPro.API.Models;

public class City
{
    public string IbgeCode { get; private set; } = null!;
    public string Name { get; init; } = null!;
    public string Uf { get; init; } = null!;
    public string UfIbgeCode => IbgeCode[..2];
 
    private City() {}
    
    public City(string ibgeCode, string name, string uf)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ibgeCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(uf);

        var code = ibgeCode.Trim();

        if (code.Length != 7 || !code.All(char.IsDigit))
            throw new ArgumentException("O código IBGE deve ter 7 dígitos.");

        IbgeCode = code;
        Name = name.Trim().ToUpperInvariant();
        Uf = uf.Trim().ToUpperInvariant();
    }
    
    
}
namespace AquaMania.Model.SerVivo;

public class SerVivoResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Nome_cientifico { get; set; }
    public string Local { get; set; }
    public string Tamanho { get; set; }
    public string Expectativa_vida { get; set; }
    public string Ph { get; set; }
    public string Temperatura { get; set; }
    public string Informacoes_adicionais { get; set; }
    public Guid Tipo_agua_id { get; set; }
    public string Tipo_agua { get; set; }
    public Guid Categoria_id { get; set; }
    public string Categoria { get; set; }
    public Guid Comportamento_id { get; set; }
    public string Comportamento { get; set; }
    public string servivo_url { get; set; }
}

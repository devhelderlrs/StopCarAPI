namespace StopCar.API.Models;

public class Veiculos(string marca, string modelo, string placa, DateTime horaEntrada)
{
    public string marca { get; set; } = marca;
    public string modelo { get; set; } = modelo;
    public string placa { get; set; } = placa;
    public DateTime horaEntrada { get; set; } = horaEntrada;
}   


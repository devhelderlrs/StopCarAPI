using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StopCar.API.Models;

int opcao = 0;
List<Veiculos> VagasAtivas = new List<Veiculos>();

if (opcao == 1 || opcao == 2 || opcao == 3)
{
    do
    {
        Console.WriteLine("O que deseja fazer?");
        Console.WriteLine("1. Entrada veículo");
        Console.WriteLine("2. Listar veículos");
        Console.WriteLine("3. Saída veículo");
        Console.WriteLine("4. Encerrar programa");

        opcao = Convert.ToInt32(Console.ReadLine());

        switch (opcao)
        {
            case 1:
                Console.WriteLine("==== STOPCAR | ENTRADA VEÍCULO ====\n");
                Console.WriteLine("Marca: ");
                string marca = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Modelo: ");
                string modelo = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Placa: ");
                string placa = Console.ReadLine() ?? string.Empty;
                DateTime horaEntrada = DateTime.Now;

                Console.WriteLine($"Veículo placa {placa.ToUpper()} cadastrado! - {DateTime.Now.ToString(@"dd/MM/yyyy hh\:mm")}");

                var veiculo = new Veiculos
                (
                    marca,
                    modelo,
                    placa,
                    horaEntrada
                );

                VagasAtivas.Add(veiculo);

                break;

            case 2:
                Console.WriteLine("==== STOPCAR | VEÍCULOS ESTACIONADOS ====\n");

                foreach (var vaga in VagasAtivas)
                {
                    Console.WriteLine($"Placa: {vaga.placa.ToUpper()} | {vaga.marca} {vaga.modelo}");
                    Console.WriteLine($"Entrada: {vaga.horaEntrada.ToString(@"hh\:mm")}\n");
                }

                break;
            case 3:
                Console.WriteLine("==== STOPCAR | SAÍDA VEÍCULOS ====\n");
                Console.WriteLine($"Digite a placa do veículo a dar saída: ");
                string consultaPlaca = Console.ReadLine() ?? string.Empty;

                if (String.IsNullOrEmpty(consultaPlaca))
                {
                    var consulta = VagasAtivas.Find(vaga => vaga.placa == consultaPlaca);

                    if (consulta != null)
                    {
                        Console.WriteLine($"Veículo: {consulta.marca} {consulta.modelo} | Placa: {consulta.placa.ToUpper()}");
                        Console.WriteLine($"Entrada: {consulta.horaEntrada}");
                        Console.WriteLine($"Confirma saída do veículo placa {consulta.placa.ToUpper()}?\n1. Sim\n2. Não");

                        int confirmacao = Convert.ToInt32(Console.ReadLine()); // Input com conversão direta para int do texto, também para condicionais.

                        switch (confirmacao)
                        {
                            case 1:
                                DateTime horaSaida = DateTime.Now.AddMinutes(120);

                                TimeSpan tempo = horaSaida - consulta.horaEntrada; //Pega a diferença de tempo entre horário de entrada e saída.
                                double minutos = Math.Round(tempo.TotalMinutes); //Transforma de TimeSpan para double, para que possa manipular em condicionais.

                                if (minutos > 0 && minutos <= 30)
                                {
                                    Console.WriteLine("Valor a pagar: R$ 5,00");
                                    Console.WriteLine($"Hora Saída: {horaSaida} | Tempo corrido: {tempo.Hours:00}h {tempo.Minutes:00}m");
                                }
                                else
                                {
                                    float fracao = 5;
                                    while (minutos >= 30)
                                    {
                                        fracao += 2;
                                        minutos -= 30;
                                    }
                                    Console.WriteLine($"Hora Saída: {horaSaida} | Tempo corrido: {tempo.Hours:00}h {tempo.Minutes:00}m");
                                    Console.WriteLine($"Valor a pagar: R$ {fracao.ToString("F2")}");
                                }
                                VagasAtivas.Remove(consulta);
                                break;
                            default:
                                Console.WriteLine("Voltando ao menu principal.");
                                break;
                        }
                    }
                    else if (consultaPlaca == "")
                    {
                        Console.WriteLine("Campo de texto vazio!");
                    }
                    else
                    {
                        Console.WriteLine("Placa inexistente!");
                    }
                }
                break;
        }
    } while (opcao > 0 && opcao <= 3);
}
else if (opcao == 4)
{
    Console.WriteLine("Encerrando aplicação.");
}
else
{
    Console.WriteLine("Opção inválida. Encerrando operação.");
}
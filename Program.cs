using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StopCar.API.Models;

int opcao;

List<Veiculos> VagasAtivas = new List<Veiculos>();
// List<Veiculos> HistoricoDia = new List<Veiculos>();

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

            Console.WriteLine($"Veículo placa {placa.ToUpper()} cadastrado! - {DateTime.Now.ToString(@"dd/MM/yyyy hh\:mm")} - Obrigado!");

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
                Console.WriteLine($"Entrada: {vaga.horaEntrada}");
                Console.WriteLine("========= STOPCAR = STOPCAR =========\n");
            }

            break;
        case 3:
            Console.WriteLine("==== STOPCAR | SAÍDA VEÍCULOS ====\n");
            Console.WriteLine($"Digite a placa do veículo a dar saída: ");
            string saida = Console.ReadLine() ?? string.Empty;

            foreach (var vagas in VagasAtivas) {
                if (saida == vagas.placa)
                {
                    Console.WriteLine($"Veículo: {vagas.marca} {vagas.modelo} | Placa: {vagas.placa}");
                    Console.WriteLine($"Entrada: {vagas.horaEntrada}");

                    DateTime horaSaida = DateTime.Now;
                    TimeSpan tempo = horaSaida - vagas.horaEntrada;

                    Console.WriteLine($"Confirma saída do veículo placa {vagas.placa.ToUpper()}?\n1. Sim\n2.Não");
                    int confirmacao = Convert.ToInt32(Console.ReadLine());
                    switch (confirmacao)
                    {
                        case 1:
                            Console.WriteLine($"Hora Saída: {horaSaida} | Tempo corrido: {tempo.TotalHours:00} {tempo.TotalMinutes:00}");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Placa não consta no cadastro.");
                }
            }
            break;
    }
} while (opcao > 0 && opcao <= 3);
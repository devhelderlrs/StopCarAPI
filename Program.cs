using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StopCar.API.Models;

int opcao;
List<Veiculos> VagasAtivas = new List<Veiculos>();

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
            string consultaPlaca = Console.ReadLine() ?? string.Empty;

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
                        DateTime horaSaida = DateTime.Now.AddMinutes(40);

                        TimeSpan tempo = horaSaida - consulta.horaEntrada; //Pega a diferença de tempo entre horário de entrada e saída.
                        double minutos = Math.Round(tempo.TotalMinutes); //Transforma de TimeSpan para double, para que possa manipular em condicionais.

                        if (minutos > 0 && minutos <= 30)
                        {
                            Console.WriteLine("Valor a pagar: R$ 5,00");
                            Console.WriteLine($"Hora Saída: {horaSaida} | Tempo corrido: {tempo.Hours:00}h {tempo.Minutes:00}m");
                        }

                        for (int cont = 0; minutos > 1; cont++)
                        {
                            if (minutos == 1)
                            {
                                double fracao = 5 + (cont * 2);
                                Console.WriteLine(fracao);
                                Console.WriteLine($"Valor a pagar: R$ {fracao}");
                                Console.WriteLine($"Hora Saída: {horaSaida} | Tempo corrido: {tempo.Hours:00}h {tempo.Minutes:00}m");
                            }
                            else
                            {
                                Console.WriteLine("Placa não encontrada.");
                                break;
                            }
                        }
                        break;
                }
            }
            else
            {

            }

            // foreach (var vagas in VagasAtivas)
            // {
            //     bool check = vagas.placa.Contains(consultaPlaca);
            //     if (check)
            //     {
            //         Console.WriteLine($"Veículo: {vagas.marca} {vagas.modelo} | Placa: {vagas.placa.ToUpper()}");
            //         Console.WriteLine($"Entrada: {vagas.horaEntrada}");
            //         Console.WriteLine($"Confirma saída do veículo placa {vagas.placa.ToUpper()}?\n1. Sim\n2. Não");

            //         int confirmacao = Convert.ToInt32(Console.ReadLine()); // Input com conversão direta para int do texto, também para condicionais.

            //         switch (confirmacao)
            //         {
            //             case 1:
            //                 DateTime horaSaida = DateTime.Now.AddMinutes(40         );

            //                 TimeSpan tempo = horaSaida - vagas.horaEntrada; //Pega a diferença de tempo entre horário de entrada e saída.
            //                 double minutos = Math.Round(tempo.TotalMinutes); //Transforma de TimeSpan para double, para que possa manipular em condicionais.

            //                 if (minutos > 0 && minutos <= 30)
            //                 {
            //                     Console.WriteLine("Valor a pagar: R$ 5,00");
            //                     Console.WriteLine($"Hora Saída: {horaSaida} | Tempo corrido: {tempo.Hours:00}h {tempo.Minutes:00}m");
            //                 }

            //                 for (int cont = 0; minutos > 1; cont++)
            //                 {
            //                     if (minutos == 1)
            //                     {
            //                         double fracao = 5 + (cont * 2);
            //                         Console.WriteLine(fracao);
            //                         Console.WriteLine($"Valor a pagar: R$ {fracao}");
            //                         Console.WriteLine($"Hora Saída: {horaSaida} | Tempo corrido: {tempo.Hours:00}h {tempo.Minutes:00}m");
            //                     }
            //                     else
            //                     {
            //                         Console.WriteLine("Placa não encontrada.");
            //                     }
            //                     break;
            //                 }
            //                 break;
            //         }
            //     }

            // }
            break;
    }
} while (opcao > 0 && opcao <= 3);
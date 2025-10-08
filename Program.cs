using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StopCar.API.Models;

int opcao = 0;
List<Veiculos> VagasAtivas = new List<Veiculos>();

Console.WriteLine("==== STOPCAR - GESTÃO DE ESTACIONAMENTO ====\n");
do
{
    Console.WriteLine("O que deseja fazer?\n");
    Console.WriteLine("1. Entrada veículo");
    Console.WriteLine("2. Listar veículos");
    Console.WriteLine("3. Saída veículo");
    Console.WriteLine("4. Encerrar programa");
    // Conversão input para int
    opcao = Convert.ToInt32(Console.ReadLine());

    switch (opcao)
    {
        case 1:
            Console.WriteLine("==== STOPCAR | ENTRADA VEÍCULO ====\n");
            Console.WriteLine("Marca: ");
            string marca = Console.ReadLine() ?? string.Empty; // Também poderia ser 'string? marca = Console.ReadLine()'
            Console.WriteLine("\nModelo: ");
            string modelo = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("\nPlaca: ");
            string placa = Console.ReadLine() ?? string.Empty;
            DateTime horaEntrada = DateTime.Now;
            // Método .ToUpper() sempre utilizado para garantir o match entre input e armazenamento.
            Console.WriteLine($"Veículo placa {placa.ToUpper()} cadastrado! - {DateTime.Now.ToString(@"dd/MM/yyyy hh\:mm")}");

            var veiculo = new Veiculos
            (   // Constructor/Instância do objeto veículo para o nosso Model.   
                marca,
                modelo,
                placa,
                horaEntrada
            );
            // Adiciona o veículo à coleção de objetos da Lista/Array.
            VagasAtivas.Add(veiculo);

            break;

        case 2:
            Console.WriteLine("==== STOPCAR | VEÍCULOS ESTACIONADOS ====\n");
            // Caso a lista/Array estivesse vazio, não queria que o código executasse, poupando processamento e reiniciando o menu principal.
            if (VagasAtivas.Count != 0)
            {
                foreach (var vaga in VagasAtivas)
                {
                    Console.WriteLine($"Placa: {vaga.placa.ToUpper()} | {vaga.marca} {vaga.modelo}");
                    Console.WriteLine($"Entrada: {vaga.horaEntrada.ToString(@"hh\:mm")}\n");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos cadastrados.");
            }
            break;
        case 3:
            Console.WriteLine("==== STOPCAR | SAÍDA VEÍCULOS ====\n");
            // Não entendi porquê da compilação não aplicar como foi no case 2, logo acima desta linha, tive de inverter.
            if (VagasAtivas.Count == 0)
            {
                Console.WriteLine("Não há veículos para dar saída.");
            }
            else
            {
                Console.WriteLine($"Digite a placa do veículo a dar saída: ");
                string consultaPlaca = (Console.ReadLine()) ?? string.Empty;
                // Após a verificação da lista, validação para saber se o input tem algum valor
                if (!String.IsNullOrEmpty(consultaPlaca.ToUpper()))
                {
                    // Retorna bool caso encontre alguma placa que contenha o input do usuário.
                    // Também poderia utilizar .Any(), mas já havia criado a lógica seguindo o retorno do .Find().
                    var consulta = VagasAtivas.Find(vaga => vaga.placa.ToUpper() == consultaPlaca.ToUpper());
                    // Caso o resultado do método .Find() for diferente de null, o programa mostrará o objeto completo do veículo da placa em questão.
                    if (consulta != null)
                    {
                        Console.WriteLine($"Veículo: {consulta.marca} {consulta.modelo} | Placa: {consulta.placa.ToUpper()}");
                        Console.WriteLine($"Entrada: {consulta.horaEntrada.ToString(@"HH\:mm")}"); //"@"HH\:mm" - para mostrar apenas Hora:minuto do DateTime.
                        Console.WriteLine($"Confirma saída do veículo placa {consulta.placa.ToUpper()}?\n1. Sim\n2. Não");
                        // Input com conversão direta do texto para int.
                        int confirmacao = Convert.ToInt32(Console.ReadLine());

                        switch (confirmacao)
                        {
                            case 1:
                                // Variável para gerar um número aleatório que vai servir como horário de saída do veículo.
                                var horaCliente = new Random();
                                // 300 = sortear um numero entre 0 e 299, que sera lido como minutos no processo e calcular o valor a ser pago na retirada do veículo.
                                DateTime horaSaida = DateTime.Now.AddMinutes(horaCliente.Next(300));
                                //Pega a diferença de tempo entre horário de entrada e saída.
                                TimeSpan tempo = horaSaida - consulta.horaEntrada;
                                //Transforma de TimeSpan para double, para que possa manipular em condicionais.
                                double minutos = Math.Round(tempo.TotalMinutes);

                                if (minutos > 0 && minutos <= 30)
                                {
                                    Console.WriteLine($"Hora entrada: {consulta.horaEntrada}");
                                    Console.WriteLine($"Hora saída: {horaSaida}\n");
                                    Console.WriteLine($"Tempo corrido: {tempo.Hours:00}h {tempo.Minutes:00}m");
                                    Console.WriteLine($"Valor a pagar: R$ 5,00");
                                }
                                else
                                {
                                    float fracao = 5;
                                    while (minutos >= 30)
                                    {
                                        //WHILE, para que, com base nos horários de entrada e saída, pudesse ter a diferença entre os dois...
                                        //...E somar 2 sempre que o loop repetisse enquanto a condição não fosse satisfeita, gerando o valor correto a ser pago.
                                        fracao += 2;
                                        minutos -= 30;
                                    }
                                    Console.WriteLine($"Hora entrada: {consulta.horaEntrada}");
                                    Console.WriteLine($"Hora saída: {horaSaida}\n");
                                    Console.WriteLine($"Tempo corrido: {tempo.Hours:00}h {tempo.Minutes:00}m");
                                    Console.WriteLine($"Valor a pagar: R$ {fracao.ToString("F2")}"); // Sintaxe para mostrar 2 casas decimais, representando melhor a moeda.
                                }
                                // Remove o objeto referente à placa do Array/Lista.
                                VagasAtivas.Remove(consulta);
                                break;
                            default:
                                Console.WriteLine("Voltando ao menu principal.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Placa inexistente!");
                    }
                }
                else if (consultaPlaca == "")
                {
                    Console.WriteLine("Campo não pode estar vazio!");
                }
            }
            break;
    }
} while (opcao > 0 && opcao <= 3);
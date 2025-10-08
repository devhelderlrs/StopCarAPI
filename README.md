# StopCarAPI
==========================================
### Proposta de projeto de gerenciamento de Estacionamento do BootCamp C# Avanade da DIO.

#### OBS.: Não segui o modelo proposto no material, pois queria implementar minha lógica manualmente.
Depois de ter quase todo o trabalho pronto, acabei dando uma olhada no conteúdo da aula e vi que estava muito abstraído, o que dificultaria muito minha busca de erros, devido a não ter basicamente nenhuma noção de C# e POO.

# Requisitos do projeto:
Criar um sistema de estacionamento, onde o usuário possa:  
1. **Cadastrar veículos**
2. **Consultar veículos cadastrados**
3. **Calcular o valor a ser pago pelo tempo de permanência do veículo no estacionamento**.  
4. **Remover veículos do cadastro**

# Minha proposta para o projeto:
* Implementar **validações seguras** de input e validações na lista de veículos; ✔️
* Garantir que loops de repetição operem **sem loop infinito**; ✔️
* Validações **required**, como o tamanho do inputString desejado para o modelo de negócio; ❎
* **Integração SQL** para uso mais intuitivo e não ficar apenas no uso do console; ❎
* Aplicação de abstração e métodos, para **melhor organização** do Program.cs; ❎

#### Explicando o código:
Modelo do veículo, encontrado na pasta `.StopCar.API/Models`:

```csharp
public class Veiculos(string marca, string modelo, string placa, DateTime horaEntrada)
{
    public string marca { get; set; } = marca;
    public string modelo { get; set; } = modelo;
    public string placa { get; set; } = placa;
    public DateTime horaEntrada { get; set; } = horaEntrada;
}   
```

**Obs.: As implementações que estão pendentes serão finalizadas posteriormente, pois estaria perdendo muito tempo para o conteúdo do bootcamp, sem interferir no que havia sido pedido neste primeiro projeto**.

 ## Dando início ao programa: 

**Passo 1**: Abra o terminal `Ctrl+'` e digite `git clone https://github.com/devhelderlrs/StopCarAPI.git`.  

**Passo 2**: Ainda com o terminal aberto e verifique se você está na pasta principal do programa com o comando `dir` ou `ls`.  
Exemplo terminal:

    Diretório: C:\Users\Helder\source\repos\Stopcar.API  

Caso não seja esteja com o caminho final "\Stopcar.API", use o comando `cd ./StopCar.API` e repita o comando anterior ao exemplo para verificar. Também é possível confirmar ao observar onde você está digitando no terminal.

**Passo 3**: No terminal, digite `dotnet run` ou, se já estiver com a ferramenta de Debug do VSCode configurada para C#, pressione `F5`.  
Deverá exibir isto no console:
```csharp
==== STOPCAR - GESTÃO DE ESTACIONAMENTO ====

O que deseja fazer?

1. Entrada veículo
2. Listar veículos
3. Saída veículo
4. Encerrar programa
```

1. `Digite 1 para cadastrar um veículo. (Carro/Moto/Utilitário)`  
2. `Digite 2 para Listar os veículos armazenados. (Caso não haja entrada de nenhum veículo deve retornar um erro, sendo necessário CADASTRAR um veículo primeiro)`  
3. `Digite 3 para remover o veículo desejado, informando a placa do mesmo. (Caso não digite nenhum valor ou a placa esteja errada da cadastrada, deverá retornar um erro)`  
4. `Digite 4 para encerrar o programa. (Qualquer resposta ao sistema neste menum, além das opções de 1 a 3 encerrará o mesmo)`

**Passo 4**: Menu 1 - Entrada veículo - Você deverá informar Marca, Modelo e, por último a respectiva Placa.  
Exemplo do terminal:
```csharp
==== STOPCAR | ENTRADA VEÍCULO ====

Marca:
Nissan // Valor Input

Modelo:
March // Valor Input

Placa:
qwe1234 // Valor Input
```

**Obs.: Somente o parâmetro **Placa** possui conversão para MAIÚSCULO, os demais campos não possuem tal verificação ou conversão.

**Passo 5**: Menu 2 - Listar veículos - Retorna a consulta à lista de veículos cadastrados.
Exemplo do terminal com 3 carros aleatórios incluídos manualmente:

```csharp
==== STOPCAR | VEÍCULOS ESTACIONADOS ====

Placa: QWE1234 | Nissan March
Entrada: 12:54 // O Sistema armazena o horário de entrada para cálculo na retirada do veículo.

Placa: ASD1234 | Chevrolet Onix
Entrada: 12:54

Placa: ZXC1234 | Fiat Toro
Entrada: 12:54
```

**Passo 6**: Saída veículo - Informe a placa do veículo desejado e o sistema mostrará o valor a ser cobrado ao cliente.

```csharp
==== STOPCAR | SAÍDA VEÍCULOS ====

Digite a placa do veículo a dar saída:
zxc1234 // Valor Input  

Veículo: Fiat Toro | Placa: ZXC1234
Entrada: 12:58

Confirma saída do veículo placa ZXC1234?
1. Sim // Executa o cálculo e remove o veículo do app.
2. Não // Ignora a placa digitada e volta ao menu principal.

1 // Valor Input  

Hora entrada: 08/10/2025 12:58:08
Hora saída: 08/10/2025 13:36:16

Tempo corrido: 00h 38m
Valor a pagar: R$ 7,00
```

**Obs.: O sistema conta com um varíavel que gera um número aleatório de minutos para diferentes horários a serem calculados cada vez que você remove um veículo. Ela pode ser alterada em:  

`StopCar.API/Program.cs - Linha 92 (declaração) e 94 (randomizador entre 0 e 299)`  

**Passo 7**: Encerrar programa - Digite 4 para encerrar ou qualquer outro dígito além das opções de 1 a 3.

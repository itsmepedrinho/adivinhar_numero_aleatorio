using System;                       // Importa recursos básicos: Console, Random, DateTime, Math
using System.Collections.Generic;   // Importa List<string> e List<int>
using System.IO;                    // Importa File (leitura e escrita em arquivo)

class Program                        // Declara a classe principal do programa
{
    static void Main()               // Ponto de entrada: onde o programa começa a rodar
    {
        Random rand = new Random();  // Cria o gerador de números aleatórios (uma vez só)
        bool programaAberto = true;  // Flag que controla se o menu continua ou fecha

        Console.WriteLine("=== JOGO DA ADIVINHAÇÃO - RANKING ==="); // Título do jogo

        while (programaAberto)       // Fica no menu até o usuário escolher "Sair"
        {
            Console.WriteLine("\n=== MENU ===");          // Cabeçalho do menu
            Console.WriteLine("1 - Jogar");               // Opção 1
            Console.WriteLine("2 - Ver histórico");       // Opção 2
            Console.WriteLine("3 - Hall da fama (top 3)"); // Opção 3
            Console.WriteLine("4 - Sair");                // Opção 4

            int opcao = LerNumero("Escolha: ");           // Lê a escolha (com validação)

            switch (opcao)                                 // Desvia pra função certa
            {
                case 1:                                    // Se escolheu 1...
                    JogarPartida(rand);                    // ...começa uma partida
                    break;                                 // Sai do switch
                case 2:                                    // Se escolheu 2...
                    VerHistorico();                        // ...mostra o arquivo cru
                    break;                                 // Sai do switch
                case 3:                                    // Se escolheu 3...
                    VerHallDaFama();                       // ...mostra o top 3
                    break;                                 // Sai do switch
                case 4:                                    // Se escolheu 4...
                    programaAberto = false;                // ...marca pra sair do while
                    break;                                 // Sai do switch
                default:                                   // Qualquer outro número...
                    Console.WriteLine("Opção inválida! Escolha de 1 a 4."); // ...avisa
                    break;                                 // Sai do switch
            }
        }

        Console.WriteLine("\nValeu por jogar! Até a próxima."); // Despedida final
    }

    static void JogarPartida(Random rand)               // Executa uma partida completa
    {
        List<string> nomes = new List<string>();         // Lista paralela: nomes dos jogadores
        List<int> tentativas = new List<int>();          // Lista paralela: tentativas de cada um
        bool continuar = true;                           // Controla se ainda tem jogador pra jogar

        int limiteMaximo = LerNumero("\nQual o número máximo? (ex: 100, 500, 1000): "); // Pergunta o limite

        while (limiteMaximo < 2)                         // Enquanto o limite for inválido...
        {
            Console.WriteLine("O limite precisa ser pelo menos 2.");        // ...avisa
            limiteMaximo = LerNumero("Qual o número máximo? ");             // ...e pede de novo
        }

        Console.WriteLine($"\nOk! O número secreto estará entre 1 e {limiteMaximo}."); // Confirma o intervalo

        while (continuar)                                // Loop dos jogadores desta partida
        {
            Console.Write("\nNome do jogador: ");        // Pede o nome
            string nome = Console.ReadLine();            // Lê o nome digitado

            if (string.IsNullOrWhiteSpace(nome))         // Se digitou vazio ou só espaços...
                nome = "Anônimo";                        // ...usa um nome padrão

            int secreto = rand.Next(1, limiteMaximo + 1); // Sorteia o número (+1 porque o máximo é exclusivo)

            int chute = 0;                               // Chute começa em 0 (nunca igual ao secreto)
            int contador = 0;                            // Zera o contador de tentativas

            Console.WriteLine($"{nome}, tente acertar o número entre 1 e {limiteMaximo}!"); // Instrui o jogador

            while (chute != secreto)                     // Repete até acertar o número secreto
            {
                chute = LerNumero("Seu chute: ");        // Lê o chute (com validação)
                contador++;                              // Soma 1 nas tentativas

                if (chute < secreto)                     // Se o chute foi baixo demais...
                    Console.WriteLine("Muito baixo!");   // ...avisa
                else if (chute > secreto)                // Se foi alto demais...
                    Console.WriteLine("Muito alto!");    // ...avisa
                else                                     // Se acertou...
                    Console.WriteLine($"Acertou em {contador} tentativa(s)! 🎉"); // ...parabeniza
            }

            nomes.Add(nome);                             // Guarda o nome do jogador
            tentativas.Add(contador);                    // Guarda quantas tentativas ele levou

            Console.Write("\nOutro jogador vai jogar? (s/n): "); // Pergunta se tem mais alguém
            string resposta = Console.ReadLine().ToLower();       // Lê a resposta em minúscula

            if (resposta != "s")                         // Se a resposta NÃO for "s"...
                continuar = false;                       // ...encerra o loop de jogadores
        }

        Console.WriteLine("\n=== RANKING ===");          // Cabeçalho do ranking

        OrdenarPorTentativas(nomes, tentativas);         // Ordena as duas listas juntas

        for (int i = 0; i < nomes.Count; i++)            // Percorre cada jogador
        {
            Console.WriteLine($"{i + 1}º lugar: {nomes[i]} - {tentativas[i]} tentativa(s)"); // Mostra a posição
        }

        SalvarNoHistorico(nomes, tentativas, limiteMaximo); // Salva esta partida no arquivo
    }

    static void VerHistorico()                           // Mostra o conteúdo cru do arquivo
    {
        Console.WriteLine("\n=== HISTÓRICO DE PARTIDAS ==="); // Cabeçalho

        if (!File.Exists("historico.txt"))               // Se o arquivo ainda não existe...
        {
            Console.WriteLine("Nenhuma partida registrada ainda."); // ...avisa
            return;                                      // ...e sai da função
        }

        string conteudo = File.ReadAllText("historico.txt"); // Lê o arquivo inteiro
        Console.WriteLine(conteudo);                     // Joga o conteúdo na tela
    }

    static void VerHallDaFama()                          // Mostra o top 3 de todas as partidas
    {
        Console.WriteLine("\n=== HALL DA FAMA (TOP 3) ==="); // Cabeçalho

        if (!File.Exists("historico.txt"))               // Se não existe arquivo...
        {
            Console.WriteLine("Nenhuma partida registrada ainda."); // ...avisa
            return;                                      // ...e sai
        }

        string[] linhas = File.ReadAllLines("historico.txt"); // Lê cada linha num array

        List<string> nomesCampeoes = new List<string>();       // Acumula nomes de todas as partidas
        List<int> tentativasCampeoes = new List<int>();        // Acumula tentativas de todas as partidas

        foreach (string linha in linhas)                 // Percorre cada linha do arquivo
        {
            if (linha.StartsWith("---") || string.IsNullOrWhiteSpace(linha)) // Se for cabeçalho ou vazia...
                continue;                                // ...pula pra próxima

            string[] partes = linha.Split(';');          // Quebra a linha em partes pelo ";"

            if (partes.Length != 3)                      // Se não tem exatamente 3 partes...
                continue;                                // ...não é do formato esperado, pula

            string nome = partes[1];                     // A parte 1 é o nome do jogador

            string parteTentativas = partes[2].Replace(" tentativa(s)", ""); // Tira o sufixo do texto
            int tent;                                    // Variável pra guardar o número convertido

            if (!int.TryParse(parteTentativas, out tent)) // Se não conseguir converter pra inteiro...
                continue;                                // ...pula essa linha

            nomesCampeoes.Add(nome);                     // Adiciona o nome na lista geral
            tentativasCampeoes.Add(tent);                // Adiciona as tentativas na lista geral
        }

        if (nomesCampeoes.Count == 0)                    // Se nada válido foi lido...
        {
            Console.WriteLine("Nenhuma partida válida encontrada."); // ...avisa
            return;                                      // ...e sai
        }

        for (int i = 0; i < tentativasCampeoes.Count - 1; i++)       // Passadas do bubble sort
        {
            for (int j = 0; j < tentativasCampeoes.Count - 1 - i; j++) // Compara pares vizinhos
            {
                if (tentativasCampeoes[j] > tentativasCampeoes[j + 1]) // Se estão fora de ordem...
                {
                    int tempT = tentativasCampeoes[j];               // Guarda a tentativa da esquerda
                    tentativasCampeoes[j] = tentativasCampeoes[j + 1]; // Move a da direita pra esquerda
                    tentativasCampeoes[j + 1] = tempT;               // Coloca a guardada na direita

                    string tempN = nomesCampeoes[j];                 // Guarda o nome da esquerda
                    nomesCampeoes[j] = nomesCampeoes[j + 1];         // Move o da direita pra esquerda
                    nomesCampeoes[j + 1] = tempN;                    // Coloca o guardado na direita
                }
            }
        }

        int quantosMostrar = Math.Min(3, nomesCampeoes.Count); // No máximo 3, ou menos se tiver menos

        for (int i = 0; i < quantosMostrar; i++)         // Percorre só os que vão ser mostrados
        {
            Console.WriteLine($"{i + 1}º lugar: {nomesCampeoes[i]} - {tentativasCampeoes[i]} tentativa(s)"); // Mostra
        }
    }

    static int LerNumero(string mensagem)                // Lê um número, insistindo se for inválido
    {
        int numero;                                      // Variável que vai receber o valor convertido

        while (true)                                     // Loop infinito (só sai quando retornar)
        {
            Console.Write(mensagem);                     // Mostra o texto pedido pelo chamador
            string entrada = Console.ReadLine();         // Lê o que o usuário digitou

            if (int.TryParse(entrada, out numero))       // Tenta converter pra inteiro
                return numero;                           // Se conseguiu, sai da função com o valor

            Console.WriteLine("Valor inválido! Digite apenas números."); // Se não, avisa
        }
    }

    static void OrdenarPorTentativas(List<string> nomes, List<int> tentativas) // Bubble sort paralelo
    {
        for (int i = 0; i < tentativas.Count - 1; i++)       // Quantas passadas pelo vetor
        {
            for (int j = 0; j < tentativas.Count - 1 - i; j++) // Compara pares vizinhos
            {
                if (tentativas[j] > tentativas[j + 1])       // Se estão fora de ordem...
                {
                    int tempT = tentativas[j];               // Guarda a da esquerda
                    tentativas[j] = tentativas[j + 1];       // Traz a da direita
                    tentativas[j + 1] = tempT;               // Coloca a guardada na direita

                    string tempN = nomes[j];                 // Guarda o nome da esquerda
                    nomes[j] = nomes[j + 1];                 // Traz o da direita
                    nomes[j + 1] = tempN;                    // Coloca o guardado na direita
                }
            }
        }
    }

    static void SalvarNoHistorico(List<string> nomes, List<int> tentativas, int limite) // Grava a partida
    {
        string dataHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm"); // Data e hora atuais formatadas

        List<string> linhas = new List<string>();        // Lista pra montar as linhas antes de gravar

        linhas.Add($"--- Partida em {dataHora} (limite {limite}) ---"); // Cabeçalho da partida

        for (int i = 0; i < nomes.Count; i++)            // Percorre os jogadores
        {
            linhas.Add($"{i + 1}º;{nomes[i]};{tentativas[i]} tentativa(s)"); // Monta uma linha por jogador
        }

        linhas.Add("");                                  // Linha em branco separando partidas

        File.AppendAllText("historico.txt", string.Join("\n", linhas) + "\n"); // Adiciona ao final do arquivo

        Console.WriteLine("\nHistórico salvo em 'historico.txt'."); // Confirma o salvamento
    }
}
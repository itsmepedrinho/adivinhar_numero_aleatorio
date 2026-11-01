# Jogo da Adivinhação

Um jogo de adivinhação feito em C# para rodar no terminal. O jogador escolhe um limite máximo, tenta descobrir um número sorteado e pode acompanhar os resultados em um ranking.

Este projeto foi desenvolvido como prática de lógica de programação e dos fundamentos de C#.

## Funcionalidades

- Menu interativo com opções para jogar, consultar o histórico, ver o hall da fama e sair.
- Sorteio de um número secreto dentro do intervalo escolhido.
- Dicas para informar se o chute foi menor ou maior que o número secreto.
- Suporte a vários jogadores na mesma partida e classificação por número de tentativas.
- Registro das partidas em `historico.txt`.
- Hall da fama com os três melhores resultados registrados.
- Validação de entradas numéricas.

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Como executar

No terminal, na pasta do projeto, execute:

```bash
dotnet run
```

Também é possível abrir `JogoAdivinhacao2.0.slnx` no Visual Studio e iniciar o projeto por lá.

## Como jogar

1. Escolha **Jogar** no menu.
2. Informe o número máximo do intervalo (pelo menos 2).
3. Digite seu nome e tente adivinhar o número secreto.
4. Ao terminar, escolha se outro jogador também quer jogar.
5. Consulte o ranking da partida ou veja o histórico e o hall da fama pelo menu.

O arquivo `historico.txt` é criado na pasta de execução quando a primeira partida é salva.

## O que pratiquei

- Tipos, variáveis, condicionais e laços de repetição.
- Métodos para organizar as operações do programa.
- Listas (`List<T>`) e ordenação de resultados.
- Validação de entrada com `int.TryParse`.
- Leitura e gravação de arquivos com `System.IO`.
- Interação com o usuário pelo `Console`.

## Melhorias futuras

- Organizar os dados de cada jogador em uma classe ou `record`.
- Separar as responsabilidades do jogo, do histórico e da interface do terminal.
- Usar LINQ para ordenar os resultados.
- Salvar e carregar o histórico em JSON.

## Tecnologias

- C#
- .NET 10

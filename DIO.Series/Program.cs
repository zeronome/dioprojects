SerieRepositorio repositorio = new SerieRepositorio();

// Main
string opcaoUsuario = ObterOpcaoUsuario();

    while (opcaoUsuario.ToUpper() != "X")
    {
        switch (opcaoUsuario)
        {
            case "1":
                ListarSeries();
                break;
            case "2":
                InserirSerie();
                break;
            case "3":
                AtualizarSerie();
                break;
            case "4":
                ExcluirSerie();
                break;
            case "5":
                VisualizarSerie();
                break;
            case "C":
                Console.Clear();
                break;

            default:
                Console.WriteLine($"{opcaoUsuario} - Opção inválida");
                break;
        }

        opcaoUsuario = ObterOpcaoUsuario();
    }

    Console.WriteLine("Obrigado por utilizar nossos serviços.");
    Console.ReadLine();


static string ObterOpcaoUsuario()
{
    Console.WriteLine();
    Console.WriteLine("DIO Séries a seu dispor!!!");
    Console.WriteLine("Informe a opção desejada:");

    Console.WriteLine("1- Listar séries");
    Console.WriteLine("2- Inserir nova série");
    Console.WriteLine("3- Atualizar série");
    Console.WriteLine("4- Excluir série");
    Console.WriteLine("5- Visualizar série");
    Console.WriteLine("C- Limpar Tela");
    Console.WriteLine("X- Sair");
    Console.WriteLine();

    string opcaoUsuario = Console.ReadLine().ToUpper();
    Console.WriteLine();
    return opcaoUsuario;
}

void ListarSeries()
{
    Console.WriteLine("Listar séries");

    var lista = repositorio.Lista();

    if (lista.Count == 0)
    {
        Console.WriteLine("Nenhuma série cadastrada.");
        return;
    }

    foreach (var serie in lista)
    {
        var excluido = serie.retornaExcluido();
        
        Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
    }
}


void InserirSerie()
{
    try
    {
        Console.WriteLine("Inserir nova série");
        foreach (int i in Enum.GetValues(typeof(Genero)))
        {
            Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
        }
        Console.Write("Digite o gênero entre as opções acima: ");
        int entradaGenero = int.Parse(Console.ReadLine());
            Console.Write("Digite o Título da Série: ");
        string entradaTitulo = Console.ReadLine();

        Console.Write("Digite o Ano de Início da Série: ");
        int entradaAno = int.Parse(Console.ReadLine());

        Console.Write("Digite a Descrição da Série: ");
        string entradaDescricao = Console.ReadLine();

        Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                    genero: (Genero)entradaGenero,
                                    titulo: entradaTitulo,
                                    ano: entradaAno,
                                    descricao: entradaDescricao);

        repositorio.Insere(novaSerie);
    }
    catch
    {
        Console.WriteLine();
        Console.Write("!!! Entrada inválida !!!");
        Console.WriteLine();
        return;
    }
}


void ExcluirSerie()
{
    if (repositorio.ProximoId() == 0)
    {
        Console.WriteLine("Nenhuma série cadastrada. Cadastre ao menos uma série para utilizar esta opção.");
        return;
    }
    try
    {
        Console.Write("Digite o id da série: ");
        int indiceSerie = int.Parse(Console.ReadLine());

        repositorio.Exclui(indiceSerie);
    }
    catch
    {
        CatchPrint("exclusão");
    }
}
        

void VisualizarSerie()
{
    if (repositorio.ProximoId() == 0)
    {
        Console.WriteLine("Nenhuma série cadastrada.");
        return;
    }
    try
    {
    Console.Write("Digite o id da série: ");
    int indiceSerie = int.Parse(Console.ReadLine());

    var serie = repositorio.RetornaPorId(indiceSerie);

    Console.WriteLine(serie);
    }
    catch
    {
        CatchPrint("visualização");
    }
}


void AtualizarSerie()
{
    if (repositorio.ProximoId() == 0)
    {
        Console.WriteLine("Nenhuma série cadastrada. Cadastre ao menos uma série para utilizar esta opção.");
        return;
    }
    try
    {
        Console.Write("Digite o id da série: ");
        int indiceSerie = int.Parse(Console.ReadLine());
        while (indiceSerie > repositorio.ProximoId()-1)
        {
            CatchPrint("atualização");
            Console.Write("Digite o id da série: ");
            indiceSerie = int.Parse(Console.ReadLine());
        }

        foreach (int i in Enum.GetValues(typeof(Genero)))
        {
            Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
        }
        Console.Write("Digite o gênero entre as opções acima: ");
        int entradaGenero = int.Parse(Console.ReadLine());

        Console.Write("Digite o Título da Série: ");
        string entradaTitulo = Console.ReadLine();

        Console.Write("Digite o Ano de Início da Série: ");
        int entradaAno = int.Parse(Console.ReadLine());

        Console.Write("Digite a Descrição da Série: ");
        string entradaDescricao = Console.ReadLine();

        Serie atualizaSerie = new Serie(id: indiceSerie,
                                    genero: (Genero)entradaGenero,
                                    titulo: entradaTitulo,
                                    ano: entradaAno,
                                    descricao: entradaDescricao);

        repositorio.Atualiza(indiceSerie, atualizaSerie);
    }
    catch
    {
        CatchPrint("atualização");
    }
}

void CatchPrint(string args)
{
        Console.WriteLine();
        Console.Write("!!! Entrada inválida !!!");
        Console.WriteLine($"Ids disponíveis para {args}: {((repositorio.ProximoId() == 0) ? "Null" : ("0 - " + (repositorio.ProximoId()-1)))}");
        Console.WriteLine();
}
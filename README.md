# 🚀 Aprendendo C# e .NET

Este repositório é destinado ao aprendizado e prática dos conceitos básicos de **C#** e **.NET**. O foco está em explorar a estrutura de projetos, configuração inicial, bancos de dados e APIs RESTful.

## 📖 Conteúdo

Aqui você encontrará exemplos práticos e organizados para:  
- Estruturação de um projeto em **C# e .NET**  
- Configuração inicial e **entry points**  
- Implementação de **migrations** e integração com bancos de dados  
- Uso dos principais **HTTP Verbs** (GET, POST, PUT, DELETE) em uma API  
- Criação de DTOs, Controllers e Repositories  
- Paginação e boas práticas no desenvolvimento de APIs  

## 🛠️ Tecnologias Utilizadas

- **C#**
- **.NET 8.0**
- **Entity Framework Core** (para migrations e ORM)
- **AutoMapper** (para mapeamento de DTOs)
- **SQL Server** (banco de dados relacional)

## 🏠 Estrutura do Projeto

A estrutura do projeto está organizada da seguinte forma:

```plaintext
Aprendendo-C#
│
├── Controllers/           # Lógica de controle da API
│   ├── CategoriasController.cs
│   └── ProdutosController.cs
│
├── DTOs/                  # Objetos de transferência de dados
│   ├── ProdutoDTORequest.cs
│   └── ProdutoDTOResponse.cs
│
├── Repositories/          # Implementação dos repositórios
│   ├── IProdutoRepository.cs
│   ├── ProdutoRepository.cs
│   ├── ICategoriaRepository.cs
│   └── CategoriaRepository.cs
│
├── Pagination/            # Paginação e parâmetros de busca
│   ├── QueryStringParameters.cs
│   ├── CategoriasParameters.cs
│   └── ProdutosParameters.cs
│
├── DTOs/Mappings/         # Configuração do AutoMapper
│   └── ProdutoDTOMappingProfile.cs
│
├── Program.cs             # Arquivo principal (entry point)
├── ApiCatalogo.csproj     # Arquivo de configuração do projeto
└── README.md              # Documentação
```

## ⚙️ Pré-requisitos

Antes de executar o projeto, é necessário ter instalados:

- **.NET SDK 8.0** ou superior  
- **SQL Server** (ou outro banco configurado)  
- Uma IDE como **Visual Studio** ou **Visual Studio Code**  

## 🚀 Como Executar o Projeto

1. Clone o repositório:

   ```bash
   git clone https://github.com/Horley123/Aprendendo-C-.git
   cd Aprendendo-C-
   ```

2. Restaure os pacotes do projeto:

   ```bash
   dotnet restore
   ```

3. Aplique as migrations no banco de dados:

   ```bash
   dotnet ef database update
   ```

4. Execute a aplicação:

   ```bash
   dotnet run
   ```

5. Acesse a API localmente:  
   `http://localhost:5000` (ou conforme a porta configurada).

## 🧹 Funcionalidades

### Endpoints da API

**Categorias**  
- `GET /api/categorias` - Retorna todas as categorias  
- `POST /api/categorias` - Cria uma nova categoria  

**Produtos**  
- `GET /api/produtos` - Retorna todos os produtos  
- `POST /api/produtos` - Cria um novo produto  

Outros endpoints incluem:  
- Paginação e parâmetros via **Query Strings**  

## 📝 Contribuição

Se você deseja contribuir:  
1. Faça um **fork** do projeto.  
2. Crie uma nova branch: `git checkout -b minha-feature`  
3. Commit suas alterações: `git commit -m "Minha nova feature"`  
4. Faça o push: `git push origin minha-feature`  
5. Abra um **Pull Request**.

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## ⭐ Agradecimento

Este repositório faz parte da minha jornada de aprendizado em **C#** e **.NET**.  
Feedbacks são sempre bem-vindos! 😊

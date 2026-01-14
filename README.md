# 📚 Library API

Projeto de exemplo em **.NET** que demonstra a implementação de uma **API de biblioteca** com foco em **boas práticas**, **arquitetura em camadas** e **testes unitários com xUnit e Moq**.

---

## 🧱 Arquitetura

O projeto segue uma separação clara de responsabilidades:

* **Domain**

  * Entidades (`Book`)
  * Regras de negócio

* **Application / Services**

  * Serviços de aplicação (`BookService`)
  * DTOs (`PrecifierBooksDto`, `PrecifiedBookView`)

* **Infrastructure**

  * Repositórios (`IBookRepository`)
  * Implementações concretas de acesso a dados

* **Tests**

  * Testes unitários com **xUnit**
  * Mocks com **Moq**

---

## ⚙️ Funcionalidade Principal

### 📌 Precificação de Livros

O sistema permite **precificar livros**, adicionando um valor de frete fixo (20% sobre o preço base).

* ✔️ Precificar **um único livro**
* ✔️ Precificar **uma lista de livros**
* ✔️ Arredondamento para **2 casas decimais**

Exemplo da regra:

```
Preço com frete = Preço * 1.2
```

---

## 🧪 Testes Unitários

Os testes validam:

* Precificação correta de um livro
* Precificação correta de múltiplos livros
* Integração correta entre **Service** e **Repository (mockado)**

### Exemplo de teste

```csharp
[Fact]
public void ShouldBePossiblePrecifyABook()
{
    var fakeBook = new Book
    {
        Id = 1,
        Name = "Clean Code",
        Price = 100m
    };

    _bookRepositoryMock
        .Setup(r => r.GetBookById(1))
        .Returns(fakeBook);

    var expectedPrice = Math.Round(fakeBook.Price * 1.2m, 2);

    var result = _bookService.Precifier(1);

    Assert.NotNull(result);
    Assert.Equal(expectedPrice, result.PriceWithShipping);
}
```

---

## 🛠️ Tecnologias Utilizadas

* **.NET 6+**
* **C#**
* **xUnit** (testes)
* **Moq** (mocking)
* **Visual Studio / Rider**

---

## ▶️ Como Executar o Projeto

1. Clone o repositório

```bash
git clone <url-do-repositorio>
```

2. Restaure os pacotes

```bash
dotnet restore
```

3. Execute os testes

```bash
dotnet test
```

---

## 📌 Boas Práticas Aplicadas

* ✔️ Princípio da Responsabilidade Única (SRP)
* ✔️ Inversão de Dependência (DIP)
* ✔️ Testes isolados e determinísticos
* ✔️ Uso correto de DTOs

---

## 📄 Licença

Projeto de estudo com fins educacionais.

---

✍️ Desenvolvido para fins de aprendizado em **Testes Unitários e Arquitetura em .NET**



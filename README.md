# Loja de Diversidades

Este projeto consiste em um desafio para a criação de uma API de controle de estoque e venda de uma loja de produtos diversos.

## Ferramentas Utilizadas

### Backend

- .NET 8.0.120
- SQLite

### Frontend

- Node.js 22.15.0
- TypeScript
- React
- Tailwind CSS

## Como Rodar o Projeto

### Backend

Para rodar o backend, é necessário ir até a pasta **backend/src/Api** e executar o seguinte comando:

```bash
dotnet run
```

### Frontend

Para rodar o frontend:

1. Na pasta **frontend**, crie um arquivo chamado .env e adicione a seguinte informação:

```
VITE_API_URL=http://localhost:5211
```
Substitua **5211** pela porta em que o backend está rodando no seu computador, se necessário.

2. Ainda na pasta **frontend**, instale as dependências:
```
npm install
```

3. Em seguida, execute o comando:

```
npm run dev
```

4. Após isso, você poderá acessar a aplicação no navegador pelo endereço: **http://localhost:5173/**


## Como o Sistema Funciona

### Página de Login

Na página de login, o usuário poderá realizar a autenticação no sistema informando **e-mail** e **senha**.  
Existe um botão **Criar Conta** caso o usuário ainda não esteja cadastrado.

Para acessar a pagina acesse: **http://localhost:5173/login**

![alt text](image.png)

---

### Página de Cadastro de Cliente

Nessa página, o usuário poderá se cadastrar no sistema informando apenas **e-mail** e **senha**.

Para acessar a pagina acesse: **http://localhost:5173/register**

![alt text](image-1.png)

---

### Página de Administrador

**OBS:** Quando o backend é executado, automaticamente será criado um usuário administrador com as seguintes credenciais:  

- **E-mail:** admin@gmail.com  
- **Senha:** admin123  

Um usuário administrador é necessário para acessar esta página.

A página de administrador pode ser acessada através da URL: **[http://localhost:5173/admin](http://localhost:5173/admin)**. Ela possui diversas funcionalidades:

- **Criação de Produtos:** Informe o nome do produto, preço e uma descrição.  
- **Movimentação de Estoque:** Selecione um produto, informe se é **Entrada** ou **Saída**, e a quantidade para atualizar o estoque.  
- **Cadastro de Usuários Administradores:** Informe e-mail e senha para criar novos administradores.  
- **Consulta e Exclusão de Produtos:** É possível visualizar todos os produtos cadastrados e deletá-los, se necessário.

![alt text](image-2.png)

![alt text](image-3.png)
---

### Página de Listagem de Produtos

Nessa página, todos os produtos cadastrados no sistema são listados, mostrando:  

- Nome  
- Preço  
- Imagem genérica para representar o produto 


![alt text](image-4.png)

---

### Página de Detalhes do Produto

Nessa página, é possível visualizar as informações detalhadas de um produto:

- Nome  
- Descrição  
- Preço  
- Quantidade em estoque  

Além disso, há um botão para realizar a compra do produto.

![alt text](image-5.png)
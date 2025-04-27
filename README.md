# 🏗️ Reserva de Equipamentos API - Arquitetura DDD

## 🔍 Domain-Driven Design (DDD)

### O que é DDD?

DDD é uma abordagem de design de software que foca na complexidade do domínio de negócio, colocando a lógica de negócio no centro da arquitetura.

### Conceitos Fundamentais

- **Domínio**: Representa o núcleo da lógica de negócio
- **Camadas**:
- Domain (Regras de Negócio)
- Application (Casos de Uso)
- Infrastructure (Implementações técnicas)
- Presentation/API (Interfaces externas)

### Benefícios

- Maior alinhamento com regras de negócio
- Código mais organizado e modular
- Facilita manutenção e evolução do sistema
- Separação clara de responsabilidades

## 🌐 Visão Geral do Projeto

Sistema de Gerenciamento de Reservas e Equipamentos desenvolvido em .NET 8 utilizando arquitetura DDD

### 📡 Endpoints

#### 🔐 Autenticação

- [Detalhes do Controller de Autenticação serão adicionados aqui]

#### 👥 Usuários

- [Detalhes do Controller de Usuários serão adicionados aqui]

#### 🖥️ Equipamentos

- [Detalhes do Controller de Equipamentos serão adicionados aqui]

### 🛡️ Segurança

- Autenticação JWT
- Autorização baseada em roles
- Tratamento de exceções centralizado

### 🔒 Tecnologias

- .NET 8
- ASP.NET Core
- Entity Framework
- Swagger/OpenAPI
- Arquitetura DDD

## 📦 Estrutura do Projeto

- Domain Layer
- Application Layer
- Infrastructure Layer
- API Layer

# Documentação do Sistema de Autenticação JWT

## Visão Geral

O sistema de autenticação JWT implementado neste projeto fornece uma solução robusta para segurança baseada em tokens em aplicações ASP.NET Core. Ele consiste em várias classes que trabalham em conjunto para configurar, validar e gerenciar o processo de autenticação usando JSON Web Tokens (JWT).

## Diagrama de Fluxo

```
┌─────────────────────┐       ┌─────────────────────────┐       ┌─────────────────────────┐
│                     │       │                         │       │                         │
│  AddAutenticacaoJwt │──────▶│ParametrosDeValidacaoDeToken│───┐  │   ConfiguracaoJwt       │
│                     │       │                         │   │  │                         │
└─────────────────────┘       └─────────────────────────┘   │  └─────────────────────────┘
         │                                                  │
         │                                                  │
         ▼                                                  │
┌─────────────────────┐                                     │
│                     │                                     │
│   Middleware JWT    │◀────────────────────────────────────┘
│                     │
└─────────────────────┘
         │
         │
         ▼
┌──────────────────────────────────────────────────────────────┐
│                                                              │
│                    Eventos de Autenticação                   │
│                                                              │
├────────────────────┬────────────────────┬───────────────────┤
│                    │                    │                   │
│TratadorDeTokenInvalido│TratadorDeAcessoNegado│TratadorDeFalhaNaAutenticacao│
│                    │                    │                   │
└────────────────────┴────────────────────┴───────────────────┘
```

## Componentes Principais

### 1. AddAutenticacaoJwt

**Responsabilidade**: Classe estática que serve como ponto de entrada para configurar todo o sistema de autenticação JWT.

**Interações**:

- Registra a classe `ConfiguracaoJwt` para injeção de dependência
- Configura o middleware de autenticação JWT Bearer
- Utiliza `ParametrosDeValidacaoDeToken` para definir os parâmetros de validação
- Configura tratadores de eventos para diferentes cenários de autenticação

### 2. ParametrosDeValidacaoDeToken

**Responsabilidade**: Define as regras para validação dos tokens JWT.

**Interações**:

- Obtém configurações da seção "Jwt" do arquivo de configuração
- Cria e configura um objeto `TokenValidationParameters` com as regras de validação
- Utiliza a classe `ConfiguracaoJwt` para acessar as configurações específicas (chave, emissor, audiência)

### 3. ConfiguracaoJwt

**Responsabilidade**: Classe de modelo que armazena as configurações JWT usadas pelo sistema.

**Propriedades**:

- `Key`: A chave secreta usada para assinar e verificar tokens
- `Issuer`: O emissor confiável do token
- `Audience`: A audiência prevista para o token

### 4. Tratadores de Eventos

#### 4.1. TratadorDeTokenInvalido

**Responsabilidade**: Trata casos onde o token está ausente, mal formatado ou inválido.

**Interações**:

- Registrado para o evento `OnChallenge` do middleware JWT Bearer
- Personaliza a resposta HTTP retornada quando o token é inválido
- Define o código de status 401 e retorna mensagem de erro em JSON

#### 4.2. TratadorDeAcessoNegado

**Responsabilidade**: Trata casos onde o usuário está autenticado mas não tem permissão para acessar um recurso.

**Interações**:

- Registrado para o evento `OnForbidden` do middleware JWT Bearer
- Personaliza a resposta HTTP retornada quando o acesso é negado
- Define o código de status 403 e retorna mensagem de erro em JSON

#### 4.3. TratadorDeFalhaNaAutenticacao

**Responsabilidade**: Trata falhas diversas durante o processo de autenticação, especialmente tokens expirados.

**Interações**:

- Registrado para o evento `OnAuthenticationFailed` do middleware JWT Bearer
- Detecta o tipo específico de falha (ex: token expirado)
- Personaliza a resposta HTTP baseada no tipo de falha
- Define o código de status 401 com mensagens de erro apropriadas em JSON

## Fluxo de Funcionamento

1. Durante a inicialização da aplicação, `AddAutenticacaoJwt.Configurar()` é chamado para registrar os serviços de autenticação.

2. O método obtém as configurações JWT do arquivo de configuração e registra a classe `ConfiguracaoJwt` para injeção de dependência.

3. O middleware de autenticação JWT Bearer é configurado com:

   - Parâmetros de validação de token criados por `ParametrosDeValidacaoDeToken.Parametros()`
   - Tratadores de eventos personalizados para diferentes cenários de autenticação

4. Quando uma requisição é recebida com um token JWT:

   - O middleware intercepta a requisição e valida o token usando os parâmetros definidos
   - Se o token for válido, o usuário é autenticado e a requisição continua
   - Se ocorrer qualquer problema, um dos tratadores de eventos é acionado:
     - `TratadorDeTokenInvalido`: Para tokens ausentes ou mal formatados
     - `TratadorDeAcessoNegado`: Para usuários sem permissão
     - `TratadorDeFalhaNaAutenticacao`: Para tokens expirados ou outras falhas

5. O tratador apropriado personaliza a resposta HTTP com um código de status e mensagem de erro relevantes.

## Exemplo de Configuração no arquivo appsettings.json

```json
{
  "Jwt": {
    "Key": "chave-secreta-muito-segura-com-pelo-menos-32-caracteres",
    "Issuer": "MeuSistema",
    "Audience": "MeusClientes"
  }
}
```

## Integração no Startup.cs ou Program.cs

```csharp
// Em ConfigureServices ou na configuração de serviços
services.AddControllers();
// Outras configurações...
AddAutenticacaoJwt.Configurar(services, Configuration);

// Em Configure ou na configuração de middleware
app.UseAuthentication(); // Deve vir antes de UseAuthorization
app.UseAuthorization();
```

## Considerações de Segurança

- A chave secreta (`Key`) deve ser suficientemente longa e complexa
- Em ambientes de produção, a chave não deve estar no código ou arquivo de configuração, mas armazenada de forma segura (ex: Azure Key Vault, AWS Secrets Manager)
- Considere definir um tempo de expiração adequado para os tokens
- Para segurança adicional, considere implementar refresh tokens e lista de revogação de tokens

## Casos de Uso

### Autenticação Bem-sucedida

1. Cliente envia credenciais válidas
2. Servidor valida credenciais e gera um token JWT
3. Cliente armazena o token e o envia em requisições subsequentes
4. Servidor valida o token e permite acesso aos recursos protegidos

### Token Inválido ou Ausente

1. Cliente envia uma requisição sem token ou com token malformado
2. Middleware JWT intercepta a requisição
3. `TratadorDeTokenInvalido` é acionado
4. Servidor retorna status 401 com mensagem "Token inválido ou não fornecido"

### Token Expirado

1. Cliente envia uma requisição com token expirado
2. Middleware JWT intercepta e verifica o token
3. `TratadorDeFalhaNaAutenticacao` detecta token expirado
4. Servidor retorna status 401 com mensagem "Seu token expirou. Faça login novamente"

### Acesso Negado

1. Cliente envia uma requisição com token válido
2. Usuário está autenticado mas não tem permissão para o recurso
3. `TratadorDeAcessoNegado` é acionado
4. Servidor retorna status 403 com mensagem "Você não tem permissão para acessar este recurso"

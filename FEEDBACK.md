# Feedback do Instrutor

#### 23/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Separação de responsabilidades
- Demonstrou conhecimento em Identity e JWT.
- Mostrou entendimento do ecossistema de desenvolvimento em .NET

## Pontos Negativos:

- A separação das camadas é excessiva e possui erros de design:
    - A camada de dominio está anemica
    - Serviços fazem parte de dominio (ou application) e não precisa de uma camada exclusiva
    - Os serviços apenas utilizam o repositorio, não encontrei logica de negocios.
    - A camada de dominio nunca pode depender de identity. A responsabilidade de login etc é da camada de aplicação (web).O Autor deve ser associado a um usuário mas não por herança.
- A validação se o usuario pode alterar um post está ruim, pode melhorar e está faltando o cenário de quando o user é admin
- Não faz sentido trazer a classe startup de volta
- O projeto não está 100% completo    


## Sugestões:

- Unificar a criação do user + autor no mesmo processo. Utilize o ID do registro do Identity como o ID da PK do Autor, assim você mantém um link lógico entre os elementos.
- Simplificar a arquitetura 3 camadas (web, api, core) resolveriam tudo devido a baixa complexidade.
- Finalizar o projeto focando nos recursos de segurança e validação de cenários (logica de negocios)

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.

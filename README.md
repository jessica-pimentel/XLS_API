# API de Configuração e Criação de Arquivos XLS

Esta API é responsável por gerenciar configurações e criar arquivos XLS (Excel). Ela oferece funcionalidades para configurar parâmetros específicos e gerar arquivos XLS com base nessas configurações.

## Funcionalidades

- **Configuração Dinâmica:** Permite configurar diversos aspectos do processo de geração de arquivos XLS, como formatação, estilos, campos obrigatórios, entre outros.
- **Criação de Arquivos XLS:** Gera arquivos XLS com base nas configurações fornecidas, incluindo dados dinâmicos provenientes de fontes de dados externas.
- **Endpoint RESTful:** Oferece uma interface RESTful para interação com a API, permitindo integração fácil com outros sistemas.

## Pacotes Utilizados para XLS

- [EPPlus](https://github.com/EPPlusSoftware/EPPlus): Biblioteca para geração e manipulação de arquivos Excel (.xlsx) em .NET.
- [OfficeOpenXml](https://github.com/JanKallman/EPPlus): API de alto desempenho para criação e manipulação de documentos do Excel (.xlsx) sem usar o Microsoft Office.

# API de Configuração e Criação de Arquivos XLS

Essa API é responsável por gerenciar configurações e criar arquivos XLS (Excel) e PDF. Ela oferece funcionalidades para configurar parâmetros específicos e gerar arquivos XLS e PDF com base nessas configurações.

## Funcionalidades

- **Configuração Dinâmica:** Permite configurar diversos aspectos do processo de geração de arquivos XLS, como formatação, estilos, campos obrigatórios, entre outros.
- **Configuração Dinâmica:** Permite configurar diversos aspectos do processo de geração de arquivos PDF, como formatação, estilos, campos obrigatórios, entre outros.
- **Criação de Arquivos XLS e PDF:** Gera arquivos XLS com base nas configurações fornecidas, incluindo dados dinâmicos provenientes de fontes de dados externas.
- **Endpoint RESTful:** Oferece uma interface RESTful para interação com a API, permitindo integração fácil com outros sistemas.

## Pacotes Utilizados para XLS 

- [EPPlus](https://github.com/EPPlusSoftware/EPPlus): Biblioteca para geração e manipulação de arquivos Excel (.xlsx) em .NET.
- [OfficeOpenXml](https://github.com/JanKallman/EPPlus): API de alto desempenho para criação e manipulação de documentos do Excel (.xlsx) sem usar o Microsoft Office.

## Pacotes Utilizados para PDF 

- **[iText 7](https://itextpdf.com/)**: Uma biblioteca poderosa e versátil em C# para criar e manipular arquivos PDF, permite a geração de documentos PDF complexos e suporta funcionalidades como manipulação de texto, imagens, tabelas e muito mais.
- **[iText.Kernel](https://itextpdf.com/en/products/itext-7/itext-7-core)**: Parte do iText 7, o núcleo que provê funcionalidades fundamentais para a manipulação de PDF.
- **[iText.Layout](https://itextpdf.com/en/products/itext-7/itext-7-core)**: Também parte do iText 7, esta biblioteca é usada para manipular elementos de alto nível como parágrafos, listas e tabelas no documento PDF.

## Pacotes Utilizados para Mail 

- **System.Net.Mail**: Namespace que fornece classes para enviar e-mails através do protocolo SMTP (Simple Mail Transfer Protocol), incluindo `MailMessage` para representar um e-mail, `SmtpClient` para enviar e-mails e `Attachment` para lidar com anexos.
- **System.Net**: Namespace que fornece classes para trabalhar com redes em geral, incluindo `NetworkCredential` para fornecer credenciais de autenticação para o servidor SMTP.
- **System.Text**: Namespace usado para manipular codificações de texto, e é usado no código fornecido para especificar a codificação UTF-8 ao definir o remetente do e-mail.

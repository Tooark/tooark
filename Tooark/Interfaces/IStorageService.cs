using Microsoft.AspNetCore.Http;
using Tooark.Dtos;
using Tooark.Exceptions;

namespace Tooark.Interfaces;

/// <summary>
/// Interface para o serviço de armazenamento.
/// </summary>
public interface IStorageService
{
  /// <summary>
  /// Cria um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="filePath">Caminho do arquivo para upload. Em formato <see cref="string"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="ArgumentException">Quando o caminho do arquivo é nulo ou vazio.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<StorageResponseDto> Create(string filePath, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Cria um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="fileUpload">Arquivo para upload. Em formato <see cref="IFormFile"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<StorageResponseDto> Create(IFormFile fileUpload, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Cria um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="fileStream">Arquivo em memória para upload. Em formato <see cref="MemoryStream"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<StorageResponseDto> Create(MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Atualiza um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="oldFile">Arquivo no bucket a ser substituído.</param>
  /// <param name="filePath">Caminho do arquivo para upload. Em formato <see cref="string"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="ArgumentException">Quando o caminho do arquivo é nulo ou vazio.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao apagar o arquivo antigo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<StorageResponseDto> Update(string oldFile, string filePath, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Atualiza um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="oldFile">Arquivo no bucket a ser substituído.</param>
  /// <param name="fileUpload">Arquivo para upload. Em formato <see cref="IFormFile"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso ocorra um erro ao apagar o arquivo antigo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<StorageResponseDto> Update(string oldFile, IFormFile fileUpload, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Atualiza um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="oldFile">Arquivo no bucket a ser substituído.</param>
  /// <param name="fileStream">Arquivo em memória para upload. Em formato <see cref="MemoryStream"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso ocorra um erro ao apagar o arquivo antigo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<StorageResponseDto> Update(string oldFile, MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Deleta um arquivo de modo Assíncrono.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <exception cref="AppException">Caso ocorra um erro ao apagar o arquivo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor.</exception>
  Task Delete(string fileName, string? bucketName = null);

  /// <summary>
  /// Realiza o download de um arquivo para memória de modo Assíncrono.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <returns>Arquivo em formato <see cref="Stream"/></returns>
  /// <exception cref="AppException">Caso ocorra um erro ao baixar o arquivo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor.</exception>
  Task<Stream> Download(string fileName, string? bucketName = null);
}

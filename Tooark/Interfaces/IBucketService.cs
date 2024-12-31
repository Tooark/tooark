using Microsoft.AspNetCore.Http;
using Tooark.Dtos;
using Tooark.Exceptions;

namespace Tooark.Interfaces;

public interface IBucketService
{
  /// <summary>
  /// Cria um arquivo no bucket.
  /// </summary>
  /// <param name="filePath">Caminho do arquivo para upload. Em formato <see cref="string"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.BadRequest">Quando o caminho do arquivo é nulo ou vazio.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<BucketResponseDto> Create(string filePath, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Cria um arquivo no bucket.
  /// </summary>
  /// <param name="fileUpload">Arquivo para upload. Em formato <see cref="IFormFile"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<BucketResponseDto> Create(IFormFile fileUpload, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Cria um arquivo no bucket.
  /// </summary>
  /// <param name="fileStream">Arquivo em memória para upload. Em formato <see cref="MemoryStream"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<BucketResponseDto> Create(MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Atualiza um arquivo no bucket.
  /// </summary>
  /// <param name="oldFile">Url do arquivo no bucket a ser substituído.</param>
  /// <param name="filePath">Caminho do arquivo para upload. Em formato <see cref="string"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.BadRequest">Quando o caminho do arquivo é nulo ou vazio.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<BucketResponseDto> Update(string oldFile, string filePath, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Atualiza um arquivo no bucket.
  /// </summary>
  /// <param name="oldFile">Url do arquivo no bucket a ser substituído.</param>
  /// <param name="fileUpload">Arquivo para upload. Em formato <see cref="IFormFile"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<BucketResponseDto> Update(string oldFile, IFormFile fileUpload, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Atualiza um arquivo no bucket.
  /// </summary>
  /// <param name="oldFile">Url do arquivo no bucket a ser substituído.</param>
  /// <param name="fileStream">Arquivo em memória para upload. Em formato <see cref="MemoryStream"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  Task<BucketResponseDto> Update(string oldFile, MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Deleta um arquivo.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao apagar o arquivo.</exception>
  Task Delete(string fileName, string? bucketName = null);

  /// <summary>
  /// Realiza o download de um arquivo para memória.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <returns>Arquivo em formato <see cref="Stream"/></returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao baixar o arquivo.</exception>
  Task<Stream> Download(string fileName, string? bucketName = null);
}

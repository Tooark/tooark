using Tooark.Exceptions;
using Tooark.Storages.Dtos;

namespace Tooark.Storages.Interfaces;

/// <summary>
/// Interface IStorageService que contém os métodos para gerenciamento de arquivos em um bucket.
/// </summary>
public interface IStorageService
{
  /// <summary>
  /// Função para fazer upload de um arquivo em um bucket.
  /// </summary>
  /// <param name="fileStream">O stream do arquivo a ser enviado.</param>
  /// <param name="fileName">O nome do arquivo a ser enviado.</param>
  /// <param name="contentType">O tipo do conteúdo do arquivo a ser enviado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será enviado. Parâmetro opcional.</param>
  /// <returns>Os dados do arquivo enviado <see cref="UploadResponseDto"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o bucket não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao enviar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  Task<UploadResponseDto> Upload(MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null);

  /// <summary>
  /// Função para apagar um arquivo do bucket.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser apagado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será apagado. Parâmetro opcional.</param>
  /// <returns>Uma mensagem com o resultado da operação.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao apagar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  Task<string> Delete(string fileName, string? bucketName = null);

  /// <summary>
  /// Função para fazer download de um arquivo do bucket.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser baixado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será baixado. Parâmetro opcional.</param>
  /// <returns>O arquivo baixo em <see cref="Stream"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao baixar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  Task<Stream> Download(string fileName, string? bucketName = null);

  /// <summary>
  /// Função para gerar um link assinado temporário do arquivo.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser assinado.</param>
  /// <param name="expiresMinutes">Tempo de expiração do link em minutos. Parâmetro opcional.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será assinado. Parâmetro opcional.</param>
  /// <returns>Uma URL assinada temporária do arquivo. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao baixar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  Task<string> Signer(string fileName, int? expiresMinutes = 0, string? bucketName = null);
}

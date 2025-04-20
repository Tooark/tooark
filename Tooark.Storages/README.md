# Exemplos de configuração para os diferentes provedores de armazenamento.

```json
{
  "Storage": {
    "BucketName": "my-bucket",
    "FileSize": 4096,
    "SignerMinutes": 10,
    "CloudProvider": "Amazon",
    "AWS": {
      "AccessKey": "your-access-key",
      "SecretKey": "your-secret-key"
    },
    "GCP": {
      "ProjectId": "your-project-id",
      "CredentialsPath": "path/to/credentials.json"
    }
  }
}
```

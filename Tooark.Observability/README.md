# Tooark.Observability

Biblioteca de observabilidade para aplicações .NET, fornecendo integração simplificada com **OpenTelemetry** para coleta de **traces**, **metrics** e **logs** com configuração via `appsettings.json` e defaults sensatos.

## 📦 Conteúdo do Pacote

### Classes de Configuração (Options)

| Classe                 | Descrição                                                    |
| ---------------------- | ------------------------------------------------------------ |
| `ObservabilityOptions` | Configurações principais de Observability                    |
| `TracingOptions`       | Configurações de rastreamento (tracing)                      |
| `MetricsOptions`       | Configurações de métricas                                    |
| `LoggingOptions`       | Configurações de logging                                     |
| `OtlpOptions`          | Configurações do exportador OTLP                             |
| `OtlpBatchOptions`     | Configurações do processador Batch (OTLP)                    |
| `DataSensitiveOptions` | Configurações de sanitização de dados sensíveis para Tracing |

### Enumerações

| Enum             | Descrição                                         |
| ---------------- | ------------------------------------------------- |
| `EProtocolOtlp`  | Protocolo de comunicação OTLP (grpc, http)        |
| `EProcessorType` | Tipo de processador de exportação (batch, simple) |

### Extensões de Injeção de Dependência

| Método                     | Descrição                                          |
| -------------------------- | -------------------------------------------------- |
| `AddTooarkOpenTelemetry()` | Configura OpenTelemetry com traces, metrics e logs |
| `AddTooarkObservability()` | Configura Observability + OpenTelemetry            |

---

## 🔧 Instalação

```bash
dotnet add package Tooark.Observability
```

---

## ⚙️ Configuração

### appsettings.json - Configuração Completa

Exemplo com **todas as opções disponíveis** no pacote:

```json
{
  "Observability": {
    "Enabled": true,
    "ServiceName": "MeuServico",
    "ServiceVersion": "1.0.0",
    "ServiceInstanceId": "instancia-001",
    "UseConsoleExporterInDevelopment": true,
    "DataSensitive": false,
    "ResourceAttributes": {
      "provider.name": "aws",
      "provider.region": "us-east-1",
      "provider.cluster": "cluster-a",
      "tenant.id": "tenant-123"
    },
    "Tracing": {
      "Enabled": true,
      "SamplingRatio": 1.0,
      "IgnorePathPrefix": "/api",
      "IgnorePaths": [
        "/health",
        "/healthz",
        "/ready",
        "/traces",
        "/metrics",
        "/logs",
        "/favicon.ico"
      ],
      "ActivitySourceName": "Tooark",
      "AdditionalSources": ["MinhaActivitySource", "OutraSource"],
      "DataSensitive": {
        "HideQueryParameters": true,
        "HideHeaders": true,
        "SensitiveRequestHeaders": [
          "authorization",
          "proxy-authorization",
          "cookie",
          "set-cookie",
          "x-api-key",
          "api-key",
          "apikey",
          "x-functions-key",
          "x-amz-security-token",
          "x-google-oauth-access-token",
          "x-azure-access-token"
        ]
      }
    },
    "Metrics": {
      "Enabled": true,
      "RuntimeMetricsEnabled": true,
      "MeterName": "Tooark",
      "AdditionalMeters": ["MeuMeter", "OutroMeter"]
    },
    "Logging": {
      "Enabled": true,
      "IncludeFormattedMessage": true,
      "IncludeScopes": true,
      "ParseStateValues": true
    },
    "Otlp": {
      "Enabled": true,
      "Endpoint": "http://localhost:4317",
      "Protocol": "grpc",
      "ExportProcessorType": "batch",
      "ServerlessOptimized": false,
      "Headers": "api-key=your-api-key,tenant-id=tenant-123",
      "Batch": {
        "MaxQueueSize": 2048,
        "MaxExportBatchSize": 512,
        "ScheduledDelayMilliseconds": 5000,
        "ExporterTimeoutMilliseconds": 30000
      }
    }
  }
}
```

### appsettings.json - Configuração Mínima

Para a maioria dos casos, uma configuração mínima é suficiente para habilitar **tracing**, **metrics** e **logging** com export via **OTLP**:

```json
{
  "Observability": {
    "ServiceName": "MeuServico",
    "Otlp": {
      "Enabled": true,
      "Endpoint": "http://localhost:4317"
    }
  }
}
```

ou com header personalizado:

```json
{
  "Observability": {
    "ServiceName": "MeuServico",
    "Otlp": {
      "Enabled": true,
      "Endpoint": "http://localhost:4317",
      "Headers": "api-key=your-api-key"
    }
  }
}
```

### appsettings.json - Configuração Serverless

Para ambientes serverless (AWS ECS, GCP Cloud Run, Azure Container Apps) com scale-to-zero:

```json
{
  "Observability": {
    "ServiceName": "MeuServico",
    "Otlp": {
      "Enabled": true,
      "Endpoint": "http://otel-collector:4317",
      "ServerlessOptimized": true
    }
  }
}
```

Para AWS Lambda, GCP Cloud Functions ou Azure Functions (envio imediato):

```json
{
  "Observability": {
    "ServiceName": "MeuServico",
    "Otlp": {
      "Enabled": true,
      "Endpoint": "http://otel-collector:4317",
      "ExportProcessorType": "simple"
    }
  }
}
```

### Program.cs

```csharp
using Tooark.Observability.Injections;

var builder = WebApplication.CreateBuilder(args);

// Adiciona OpenTelemetry com configurações do appsettings.json
builder.Services.AddTooarkOpenTelemetry(builder.Configuration);

var app = builder.Build();

app.Run();
```

### Configuração Programática

Você também pode configurar programaticamente ou combinar com `appsettings.json`:

```csharp
using Tooark.Observability.Injections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTooarkOpenTelemetry(builder.Configuration, options =>
{
    // Sobrescreve configurações do appsettings.json
    options.ServiceName = "MeuServicoCustomizado";
    options.Tracing.SamplingRatio = 0.5; // 50% dos traces

    // Adiciona sources/meters customizados
    options.Tracing.AdditionalSources = ["MinhaActivitySource"];
    options.Metrics.AdditionalMeters = ["MeuMeter"];

    // Configuração avançada via callbacks
    options.ConfigureTracing = builder =>
    {
        // Adicionar instrumentações adicionais
        // builder.AddSqlClientInstrumentation();
    };

    options.ConfigureMetrics = builder =>
    {
        // Configuração adicional de métricas
    };

    options.ConfigureLogging = loggerOptions =>
    {
        // Configuração adicional de logging
    };
});

var app = builder.Build();

app.Run();
```

---

## 📊 Opções de Configuração

### ObservabilityOptions

| Propriedade                       | Tipo                      | Padrão  | Descrição                                       |
| --------------------------------- | ------------------------- | ------- | ----------------------------------------------- |
| `Enabled`                         | bool                      | `true`  | Habilita/desabilita Observability               |
| `ServiceName`                     | string?                   | `null`  | Nome do serviço (inferido se não definido)      |
| `ServiceVersion`                  | string?                   | `null`  | Versão do serviço (inferida se não definida)    |
| `ServiceInstanceId`               | string?                   | `null`  | ID único da instância (GUID se não definido)    |
| `ResourceAttributes`              | Dictionary<string,string> | `{}`    | Atributos adicionais para Resource              |
| `DataSensitive`                   | bool                      | `false` | Permitir dados sensíveis sem sanitização global |
| `UseConsoleExporterInDevelopment` | bool                      | `true`  | Usar Console exporter em Development            |

### TracingOptions

| Propriedade          | Tipo                 | Padrão                       | Descrição                                   |
| -------------------- | -------------------- | ---------------------------- | ------------------------------------------- |
| `Enabled`            | bool                 | `true`                       | Habilita tracing                            |
| `SamplingRatio`      | double               | `1.0`                        | Taxa de amostragem (0.0-1.0)                |
| `IgnorePathPrefix`   | string?              | `null`                       | Prefixo a adicionar aos paths ignorados     |
| `IgnorePaths`        | string[]             | health, metrics, traces, etc | Paths a serem ignorados no tracing          |
| `ActivitySourceName` | string               | `Tooark`                     | Nome do ActivitySource padrão               |
| `AdditionalSources`  | string[]             | `[]`                         | ActivitySources adicionais para capturar    |
| `DataSensitive`      | DataSensitiveOptions | (defaults)                   | Configurações granulares de dados sensíveis |

### DataSensitiveOptions (Tracing)

| Propriedade               | Tipo     | Padrão                     | Descrição                           |
| ------------------------- | -------- | -------------------------- | ----------------------------------- |
| `HideQueryParameters`     | bool     | `true`                     | Remove query string do http.target  |
| `HideHeaders`             | bool     | `true`                     | Mascara headers sensíveis           |
| `SensitiveRequestHeaders` | string[] | authorization, cookie, etc | Lista de headers a serem mascarados |

### MetricsOptions

| Propriedade             | Tipo     | Padrão   | Descrição                         |
| ----------------------- | -------- | -------- | --------------------------------- |
| `Enabled`               | bool     | `true`   | Habilita métricas                 |
| `RuntimeMetricsEnabled` | bool     | `true`   | Habilita métricas de runtime .NET |
| `MeterName`             | string   | `Tooark` | Nome do Meter padrão              |
| `AdditionalMeters`      | string[] | `[]`     | Meters adicionais a registrar     |

### LoggingOptions

| Propriedade               | Tipo | Padrão | Descrição                         |
| ------------------------- | ---- | ------ | --------------------------------- |
| `Enabled`                 | bool | `true` | Habilita logging OpenTelemetry    |
| `IncludeFormattedMessage` | bool | `true` | Incluir mensagem formatada        |
| `IncludeScopes`           | bool | `true` | Incluir scopes                    |
| `ParseStateValues`        | bool | `true` | Fazer parse dos valores de estado |

### OtlpOptions

| Propriedade           | Tipo             | Padrão   | Descrição                                       |
| --------------------- | ---------------- | -------- | ----------------------------------------------- |
| `Enabled`             | bool             | `false`  | Habilita exportador OTLP                        |
| `Endpoint`            | string?          | `null`   | Endpoint do coletor (ex: http://localhost:4317) |
| `Protocol`            | EProtocolOtlp    | `grpc`   | Protocolo: `grpc` ou `http`                     |
| `ExportProcessorType` | EProcessorType   | `batch`  | Processador: `batch` ou `simple`                |
| `ServerlessOptimized` | bool             | `false`  | Otimiza batch para ambientes serverless         |
| `Headers`             | string?          | `null`   | Headers (formato: `key1=value1,key2=value2`)    |
| `Batch`               | OtlpBatchOptions | defaults | Opções do batch                                 |

### OtlpBatchOptions

| Propriedade                   | Tipo | Padrão  | Descrição                               |
| ----------------------------- | ---- | ------- | --------------------------------------- |
| `MaxQueueSize`                | int  | `2048`  | Tamanho máximo da fila interna          |
| `MaxExportBatchSize`          | int  | `512`   | Tamanho máximo do lote (≤ MaxQueueSize) |
| `ScheduledDelayMilliseconds`  | int  | `5000`  | Intervalo entre envios em lote (ms)     |
| `ExporterTimeoutMilliseconds` | int  | `30000` | Timeout do export (ms)                  |

---

## 🎯 Comportamento Padrão

### Resource (Identificação do Serviço)

O OpenTelemetry usa Resource para identificar a origem dos dados de telemetria:

| Atributo                 | Fonte                                                                     |
| ------------------------ | ------------------------------------------------------------------------- |
| `service.name`           | `ServiceName` → `AssemblyName` → `"unknown_service"`                      |
| `service.version`        | `ServiceVersion` → `AssemblyVersion` → `"unknown_version"`                |
| `service.instance.id`    | `ServiceInstanceId` → `OTEL_SERVICE_INSTANCE_ID` → `Guid.NewGuid()`       |
| `deployment.environment` | `ASPNETCORE_ENVIRONMENT` → `DOTNET_ENVIRONMENT` → `"unknown_environment"` |
| `host.name`              | `Environment.MachineName`                                                 |
| `process.pid`            | `Environment.ProcessId`                                                   |
| `process.runtime.*`      | Informações do runtime .NET                                               |

### Tracing

Quando habilitado (`Tracing.Enabled = true`):

- **ASP.NET Core Instrumentation**: captura automaticamente traces de requisições HTTP de entrada
- **HTTP Client Instrumentation**: captura traces de requisições HTTP de saída (HttpClient)
- **Filter Paths**: por padrão ignora: `/health`, `/healthz`, `/ready`, `/traces`, `/metrics`, `/logs`, `/favicon.ico`
- **RecordException**: exceções são registradas automaticamente nos spans
- **Sampling**: configurável via `SamplingRatio` (0.0 = 0%, 1.0 = 100%)

### Sanitização de Dados Sensíveis

Quando `DataSensitive = false` (padrão):

- **Query Parameters**: removidos do atributo `http.target` (ex: `/api/users?token=xxx` → `/api/users`)
- **Headers Sensíveis**: mascarados nos spans (Authorization, Cookie, API keys, etc.)

### Metrics

Quando habilitado (`Metrics.Enabled = true`):

- **ASP.NET Core Instrumentation**: métricas de requisições HTTP de entrada
- **HTTP Client Instrumentation**: métricas de chamadas HTTP de saída
- **Runtime Instrumentation**: métricas do runtime .NET (GC, threads, etc.) quando `RuntimeMetricsEnabled = true`

### Logging

Quando habilitado (`Logging.Enabled = true`):

- Integra com `Microsoft.Extensions.Logging`
- Exporta logs via OTLP junto com traces e metrics
- Correlaciona automaticamente logs com traces (TraceId/SpanId)

### Exportadores

| Exportador  | Ativação                                                                       | Tipo |
| ----------- | ------------------------------------------------------------------------------ | ---- |
| **OTLP**    | `Otlp.Enabled = true` + `Otlp.Endpoint` configurado                            | Push |
| **Console** | Ambiente `Development` + `UseConsoleExporterInDevelopment` + OTLP desabilitado | Push |

> **Nota**: Este pacote **não expõe endpoints HTTP** para coleta de métricas (como `/metrics` do Prometheus). Usa apenas o modelo **push** via OTLP.

---

## 🔌 Integração com Coletores

### OpenTelemetry Collector (gRPC)

```json
{
  "Observability": {
    "ServiceName": "MeuServico",
    "Otlp": {
      "Enabled": true,
      "Endpoint": "http://otel-collector:4317",
      "Protocol": "grpc"
    }
  }
}
```

### OpenTelemetry Collector (HTTP)

```json
{
  "Observability": {
    "ServiceName": "MeuServico",
    "Otlp": {
      "Enabled": true,
      "Endpoint": "http://otel-collector:4318",
      "Protocol": "http"
    }
  }
}
```

### Jaeger

```json
{
  "Observability": {
    "ServiceName": "MeuServico",
    "Otlp": {
      "Enabled": true,
      "Endpoint": "http://jaeger:4317",
      "Protocol": "grpc"
    }
  }
}
```

### Grafana Cloud / Tempo

```json
{
  "Observability": {
    "ServiceName": "MeuServico",
    "Otlp": {
      "Enabled": true,
      "Endpoint": "https://otlp-gateway-prod-us-central-0.grafana.net/otlp",
      "Protocol": "http",
      "Headers": "Authorization=Basic <base64-encoded-credentials>"
    }
  }
}
```

### Azure Monitor / Application Insights

Para Azure Monitor, use o pacote `Azure.Monitor.OpenTelemetry.Exporter` e configure via callback:

```csharp
builder.Services.AddTooarkOpenTelemetry(builder.Configuration, options =>
{
    options.ConfigureTracing = builder =>
    {
        builder.AddAzureMonitorTraceExporter(o =>
        {
            o.ConnectionString = "<connection-string>";
        });
    };

    options.ConfigureMetrics = builder =>
    {
        builder.AddAzureMonitorMetricExporter(o =>
        {
            o.ConnectionString = "<connection-string>";
        });
    };
});
```

---

## 🚀 Ambientes Serverless

Containers serverless podem ser desligados a qualquer momento (scale-to-zero), causando **perda de dados de telemetria** se o shutdown acontecer antes do flush do buffer.

### ServerlessOptimized

Otimiza automaticamente as configurações de batch para minimizar perda de dados:

| Parâmetro                    | Valor Padrão | Valor Serverless | Impacto                               |
| ---------------------------- | ------------ | ---------------- | ------------------------------------- |
| `ScheduledDelayMilliseconds` | 5000ms       | 1000ms           | Flush a cada 1 segundo                |
| `MaxExportBatchSize`         | 512          | 128              | Lotes menores, envios mais frequentes |
| `MaxQueueSize`               | 2048         | 512              | Menos dados em risco de perda         |

### Recomendações por Ambiente

| Cenário                                   | Configuração Recomendada              |
| ----------------------------------------- | ------------------------------------- |
| Servidores tradicionais (VMs, bare metal) | Padrão (`ServerlessOptimized: false`) |
| Kubernetes com pods persistentes          | Padrão (`ServerlessOptimized: false`) |
| AWS ECS (Fargate ou EC2)                  | `ServerlessOptimized: true`           |
| GCP Cloud Run                             | `ServerlessOptimized: true`           |
| Azure Container Apps                      | `ServerlessOptimized: true`           |
| AWS Lambda                                | `ExportProcessorType: simple`         |
| GCP Cloud Functions                       | `ExportProcessorType: simple`         |
| Azure Functions                           | `ExportProcessorType: simple`         |

### Graceful Shutdown

O OpenTelemetry SDK faz `ForceFlush` automaticamente durante o shutdown. Configure tempo suficiente:

- **AWS ECS**: `stopTimeout` no task definition (padrão: 30s)
- **GCP Cloud Run**: `terminationGracePeriodSeconds` (padrão: 10s)
- **Kubernetes**: `terminationGracePeriodSeconds` no Pod spec

---

## 📝 Exemplos de Uso

### Exemplo Completo com API

```csharp
using Tooark.Observability.Injections;

var builder = WebApplication.CreateBuilder(args);

// Configura serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura OpenTelemetry
builder.Services.AddTooarkOpenTelemetry(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

Com `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Observability": {
    "ServiceName": "MinhaAPI",
    "ServiceVersion": "1.0.0",
    "Otlp": {
      "Enabled": true,
      "Endpoint": "http://otel-collector:4317"
    }
  }
}
```

### Criando Traces Customizados

```csharp
using System.Diagnostics;

public class MeuServico
{
    private static readonly ActivitySource _activitySource = new("Tooark");

    public async Task ProcessarPedido(int pedidoId)
    {
        using var activity = _activitySource.StartActivity("ProcessarPedido");
        activity?.SetTag("pedido.id", pedidoId);

        try
        {
            // Lógica de processamento...
            activity?.SetTag("pedido.status", "sucesso");
        }
        catch (Exception ex)
        {
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            activity?.RecordException(ex);
            throw;
        }
    }
}
```

### Criando Métricas Customizadas

```csharp
using System.Diagnostics.Metrics;

public class MeuServico
{
    private static readonly Meter _meter = new("Tooark");
    private static readonly Counter<long> _pedidosProcessados = _meter.CreateCounter<long>("pedidos.processados");
    private static readonly Histogram<double> _tempoProcessamento = _meter.CreateHistogram<double>("pedidos.tempo_ms");

    public async Task ProcessarPedido(int pedidoId)
    {
        var sw = Stopwatch.StartNew();

        // Lógica de processamento...

        sw.Stop();
        _pedidosProcessados.Add(1, new KeyValuePair<string, object?>("tipo", "novo"));
        _tempoProcessamento.Record(sw.ElapsedMilliseconds);
    }
}
```

---

## 📋 Dependências

| Pacote                                         | Descrição                               |
| ---------------------------------------------- | --------------------------------------- |
| `Tooark.Exceptions`                            | Exceções customizadas                   |
| `OpenTelemetry`                                | SDK base do OpenTelemetry               |
| `OpenTelemetry.Exporter.Console`               | Exporter para console (desenvolvimento) |
| `OpenTelemetry.Exporter.OpenTelemetryProtocol` | Exporter OTLP (gRPC/HTTP)               |
| `OpenTelemetry.Extensions.Hosting`             | Integração com Host do .NET             |
| `OpenTelemetry.Instrumentation.AspNetCore`     | Instrumentação automática ASP.NET Core  |
| `OpenTelemetry.Instrumentation.Http`           | Instrumentação automática HttpClient    |
| `OpenTelemetry.Instrumentation.Runtime`        | Métricas do runtime .NET                |

---

## ⚠️ Observações Importantes

1. **Lifecycle gerenciado pelo host**: O OpenTelemetry é registrado via `services.AddOpenTelemetry()`, garantindo gerenciamento correto de `TracerProvider`, `MeterProvider` e shutdown graceful.

2. **Sem quebra silenciosa**: Se nenhum exportador estiver configurado, a aplicação continua funcionando normalmente, apenas sem exportar telemetria.

3. **Console Exporter em Development**: Ativado automaticamente quando `UseConsoleExporterInDevelopment = true` e OTLP não está configurado.

4. **Sem endpoint /metrics**: Este pacote usa modelo **push** (OTLP). Não expõe endpoints HTTP para scraping estilo Prometheus.

5. **Normalização de ResourceAttributes**: Chaves são normalizadas automaticamente: `lowercase`, espaços → `.`, caracteres inválidos → `_`.

6. **Callbacks para customização**: Use `ConfigureTracing`, `ConfigureMetrics` e `ConfigureLogging` para adicionar instrumentações ou configurações avançadas.

---

## 🪪 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests no repositório [Tooark.Observability](https://github.com/Tooark/tooark/issues).

## 📄 Licença

Este projeto está licenciado sob a licença BSD 3-Clause. Veja o arquivo [LICENSE](../LICENSE) para mais detalhes.

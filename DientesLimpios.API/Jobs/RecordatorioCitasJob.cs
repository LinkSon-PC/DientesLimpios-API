
using DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.EnviarRecordatorioCitas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.API.Jobs
{
    public class RecordatorioCitasJob : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly TimeZoneInfo zonaHoararia = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public RecordatorioCitasJob(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                var ahora = TimeZoneInfo.ConvertTimeToUtc(DateTime.UtcNow, zonaHoararia);

                if(ahora.Hour == 8)
                {
                    using var scope = scopeFactory.CreateScope();
                    var mediador = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediador.Send(new ComandoEnviarRecordatorioCitas());
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using SzafyNaLeki.Models;
using SzafyNaLeki.Services;

namespace SzafyNaLeki.Symulator
{
    public class TemperaturaSymulator : BackgroundService
    {
        private readonly ISzafaService _szafaService;
        private readonly ILogger<TemperaturaSymulator> _logger;

        public TemperaturaSymulator(ISzafaService szafaService, ILogger<TemperaturaSymulator> logger)
        {
            _szafaService = szafaService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Symulacja zmiany temperatur w szafach
                    _logger.LogInformation("Symulacja zmiany temperatur w szafach.");
                    await SymulujZmianyTemperatur();

                    // Oczekaj 1 godzinę przed następnym cyklem
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Błąd w trakcie symulacji zmian temperatur.");
                }
            }
        }

        private async Task SymulujZmianyTemperatur()
        {
            // Tutaj można dodać logikę symulacji zmian temperatur w szafach
            // Na przykład, losowanie nowych temperatur dla każdej szafy
            var szafa = _szafaService.GetById(11);
            var random = new Random();
            var dto = new AktualizujSzafeDto
            {
                Temperatura1 = random.Next(4, 12),
                Temperatura2 = random.Next(4, 12)
            };
            _szafaService.Update(11, dto);
        }
    }
}

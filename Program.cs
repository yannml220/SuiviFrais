using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.EventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace SuiviFrais
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {

                  

                    services.AddHostedService<Cloture>();
                    services.AddScheduledTask<Cloture>(c =>
                    {
                        c.TimeZoneInfo = TimeZoneInfo.Local;
                        //c.CronExpression = @"00 0 12 1 * *";         //le hosted service cloture est programmé pour cloturer les fiches du mois précédent chaque 1er du mois à 12h 00 min 00 s
                        c.CronExpression = @"30 * * * * *";           
                    });


                    services.AddHostedService<MiseEnPaiement>();
                    services.AddScheduledTask<MiseEnPaiement>(c =>
                    {
                        c.TimeZoneInfo = TimeZoneInfo.Local;
                        //c.CronExpression = @"00 0 12 20 * *";      //le hosted service miseEnPaiement est programmé pour mettre en paiement les fiches du mois précédent chaque 20 du mois à 12h 00 min 00 s
                        c.CronExpression = @"10 * * * * *";         
                    });



                });




     }
           
}

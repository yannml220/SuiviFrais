using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace SuiviFrais
{
    public class MysqlDataAccess
    {

        private readonly MySqlConnection Connection;

        public MysqlDataAccess()
        {

            var configuration = GetConfiguration();
            Connection = new MySqlConnection(configuration.GetSection("ConnectionStrings").GetSection("Default").Value);
        }









        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }



        public void showConnectionString()
        {
            var configuration = GetConfiguration();
            var con = configuration.GetSection("ConnectionStrings").GetSection("Default").Value;
            System.Console.WriteLine("connection string" + con);
        }


        public void OpenConnection()
        {
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    System.Console.WriteLine("la connexion a bien été ouverte");
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    System.Console.WriteLine("erreur" + e.Message.ToString());
                }

            }

        }


        public void CloseConnection()
        {
            using (Connection)
            {
                try
                {
                    Connection.Close();
                    System.Console.WriteLine("la connexion a bien été fermée");
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    System.Console.WriteLine("erreur" + e.Message.ToString());
                }

            }

        }


        /*public static string QueryRequest(string request)
        {
            var req = request;
            var cmd = new MySqlCommand(req,Connection);

            return Result;
        }*/



        public void UpdateDb(string request)
        {
            Connection.Open();
            System.Console.WriteLine(" the connection is " + Connection.State.ToString());
            var req = request;             //récupère  la requête 
            var cmd = new MySqlCommand(req, Connection); //créer l'objet qui transforme la requête en
            using (cmd)                                //commande 
            {
                try
                {
                    var result = cmd.ExecuteNonQuery();
                    System.Console.WriteLine("la mise à jour de la table a bien été prise en compte");

                    Connection.Close();

                    System.Console.WriteLine(" the connection is " + Connection.State.ToString());

                }
                catch (System.InvalidOperationException e)
                {
                    System.Console.WriteLine("message d'erreur :" + e.Message);
                }

            }



        }






    }
}
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CMSUserMerger
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				const string outputPath = "output.sql";

				// Initialize configuration
				var builder = new ConfigurationBuilder();
				builder.AddJsonFile("config.json", true);
				builder.AddUserSecrets();
				var cfg = builder.Build();

				// Create a SQL connection
				using (SqlConnection connection = new SqlConnection(cfg["ConnectionString"]))
				{
					connection.Open();

					// Get all FK references
					var result = connection.Query<dynamic>("EXEC sp_fkeys 'CMS_User'");

					StringBuilder sb = new StringBuilder();
					foreach (dynamic o in result)
					{
						// Skip user settings
						if (o.FKTABLE_NAME == "CMS_UserSettings") continue;

						sb.AppendLine($"UPDATE {o.FKTABLE_NAME} SET {o.FKCOLUMN_NAME} = {cfg["NewUserID"]} WHERE {o.FKCOLUMN_NAME} = {cfg["OldUserID"]}");
					}


					File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
				}

				Console.WriteLine($"SQL script successfully generated: {outputPath}");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}

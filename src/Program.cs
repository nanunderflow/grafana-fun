using System;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;

namespace Metrics
{
    [Measurement("temperature")]
    public class Temperature
    {
        [Column("location", IsTag = true)]
        public string Location { get; set; }

        [Column("value")]
        public double Value { get; set; }

        [Column(IsTimestamp = true)]
        public DateTime Time;
    }

    public static class Program
    {
        private static readonly char[] Token = "".ToCharArray();

        public static void Main(string[] args)
        {
            var client = InfluxDBClientFactory.Create("http://localhost:8086", Token);

            using (var writer = client.GetWriteApi())
            {
                var temperature = new Temperature {Location = "south", Value = 62D, Time = DateTime.UtcNow};
                writer.WriteMeasurement("bucket_name", "org_id", WritePrecision.Ns, temperature);
            }
        }
    }
}
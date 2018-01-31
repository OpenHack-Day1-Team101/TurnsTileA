using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "OHIoTTeam101.azure-devices.net";
        static string deviceKey = "KfNaI5orcEAU6iSX8iO4DUWQ3mCs22NtrMA6ZqSUPFY=";
        static string deviceConnectionString = "HostName=OHIoTTeam101.azure-devices.net;DeviceId=TurnsTileA;SharedAccessKey=KfNaI5orcEAU6iSX8iO4DUWQ3mCs22NtrMA6ZqSUPFY=";

        static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");
            //deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey), Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionString);
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {
            string ticketID = "";
            DateTime entryTime;
            Random rand = new Random();

            while (true)
            {
                var telemetryDataPoint = new
                {
                    //  messageId = messageId++,
                    deviceId = "TurnsTileA",
                    entryTime = DateTime.Now,
                    ticketID = Guid.NewGuid()
            };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await Task.Delay(1000);
            }
        }
    }
}

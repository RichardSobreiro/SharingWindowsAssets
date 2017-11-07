using System;
using System.ServiceModel;
using Topshelf;

namespace SelfHostedWCFService
{
    public class Service
    {
        public void Start()
        {
            try
            {
                svcHost = new ServiceHost(typeof(SharingWindowsAssets.WindowsAssets));
                svcHost.Open();
                Console.WriteLine("\n\nService is Running  at following address");
                Console.WriteLine("\nhttp://localhost:9001/SharingWindowsAssets");
                Console.WriteLine("\nnet.tcp://localhost:9002/SharingWindowsAssets");
            }
            catch (Exception eX)
            {
                svcHost = null;
                Console.WriteLine("Service can not be started \n\nError Message [" + eX.Message + "]");
            }
            if (svcHost != null)
            {
                Console.WriteLine("\nPress any key to close the Service");
                Console.ReadKey();
                svcHost.Close();
                svcHost = null;
            }
        }

        public void Stop()
        {
            svcHost.Close();
            svcHost = null;
        }

        private ServiceHost svcHost = null;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 //1
            {
                x.Service<Service>(s =>                        //2
                {
                    s.ConstructUsing(name => new Service());     //3
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => tc.Stop());               //5
                });
                x.RunAsLocalSystem();                            //6

                x.SetDescription("Sharing Windows assets with Peer-To-Peer applications.");        //7
                x.SetDisplayName("SharingWindowsAssets");                       //8
                x.SetServiceName("SharingWindowsAssets");                       //9
            });
        }
    }
}

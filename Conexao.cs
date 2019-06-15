using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel.Description;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
namespace NovaAplicacao.DAO
{
    class Conexao
    {

        public static void Conectar()
        {
            try
            {
                String connectionString = @"Url=https://testesmart9.api.crm2.dynamics.com/; 
                Username = daniel.ruy@testesmart9.onmicrosoft.com; 
                Password = Temp@2019; authtype=Office365";

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                CrmServiceClient getcrmServiceClient = new CrmServiceClient(connectionString);
                Program._orgService = (IOrganizationService)getcrmServiceClient.OrganizationWebProxyClient != null ?
                    (IOrganizationService)getcrmServiceClient.OrganizationWebProxyClient : 
                    (IOrganizationService)getcrmServiceClient.OrganizationServiceProxy;
                if (Program._orgService == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível conectar ao Dynamics. Erro: " + ex);
                Console.ReadLine();
                System.Environment.Exit(0);
            }
        }

    }
}


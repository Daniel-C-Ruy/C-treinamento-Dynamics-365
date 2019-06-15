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
    public class SystemUser
    {
        public static Guid RetornaGuidUsuario()
        {

            Guid userID = ((WhoAmIResponse)Program._orgService.Execute(new WhoAmIRequest())).UserId;
            return userID;
        }

        public static void ExibirInformacaoUsuario(Guid userID)
        {
            Entity entUsuario = new Entity();
            ColumnSet attributes = new ColumnSet(new string[] { "firstname", "lastname", "internalemailaddress", "businessunitid" });
            entUsuario = Program._orgService.Retrieve("systemuser", userID, attributes);

            EntityReference referenciaBU = new EntityReference();

            String nome = (String)entUsuario["firstname"];
            String sobrenome = (String)entUsuario["lastname"];
            String email = (String)entUsuario["internalemailaddress"];
            referenciaBU = (EntityReference)entUsuario["businessunitid"];

            Console.WriteLine("Nome do usuario: " + nome + " " + sobrenome + "\nEmail do usuario: " + email + "\nBU: " + referenciaBU.Name);

        }

        public static void AtualizaTelefone(Guid userID, String telefone)
        {
            Entity entUsuario = new Entity("systemuser");
            entUsuario.Id = userID;
            entUsuario["homephone"] = telefone;
            Program._orgService.Update(entUsuario);
            Console.WriteLine("Telefone atualizado!");
        }

        public static void AtualizaEmail(Guid userID, String email)
        {
            Entity entUsuario = new Entity("systemuser");
            entUsuario.Id = userID;
            entUsuario["personalemailaddress"] = email;
            Program._orgService.Update(entUsuario);
            Console.Write("Email 2 atualizado!");

        }
        
    }
}

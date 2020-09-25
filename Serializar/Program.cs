using BibliotecaBruno;
using Caelum.Stella.CSharp.Validation;
using System;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Serializar
{
    class Program
    {
        private static JavaScriptSerializer serializeJson;

        static void Main(string[] args)
        {
            Usuario usuario = new Usuario()
            {
                Nome = "Bruno Gomes",
                Cpf = "91634571304",
                Email = "bruno.gomes@fitbank.com.br"
            };

            Estabelecimento estabelecimento = new Estabelecimento()
            {
                ID = 1,
                Cnpj = "01236481616413",
                RazaoSocial = "Empresa Teste"
            };

            Estabelecimento estabelecimento2 = new Estabelecimento()
            {
                ID = 2,
                Cnpj = "41313131",
                RazaoSocial = "Empresa 02"
            };

            ValidaCpf(usuario.Cpf);

            Console.ReadKey();

            StreamWriter stream = new StreamWriter(@"C:\Users\bruno\source\repos\Serializar\xmlUsuario.xml");
            StreamWriter streamjs = new StreamWriter(@"C:\Users\bruno\source\repos\Serializar\jsUsuario.json");

            XmlSerializer serialUsuario = new XmlSerializer(typeof(Usuario));
            serialUsuario.Serialize(stream, usuario);

            XmlSerializer serialEstabelecimento = new XmlSerializer(typeof(Estabelecimento));
            serialEstabelecimento.Serialize(stream, estabelecimento);
            serialEstabelecimento.Serialize(stream, estabelecimento2);


            serializeJson = new JavaScriptSerializer();
            string objSerializeJson = serializeJson.Serialize(usuario);
            string objSerializeJson2 = serializeJson.Serialize(estabelecimento);
            string objSerializeJson3 = serializeJson.Serialize(estabelecimento2);

            streamjs.WriteLine(objSerializeJson);
            streamjs.WriteLine(objSerializeJson2);
            streamjs.WriteLine(objSerializeJson3);
            streamjs.Close();
        }

        private static void ValidaCpf(string cpf)
        {
            try
            {
                new CPFValidator().AssertValid(cpf);
                Console.WriteLine("O CPF " + cpf + " é válido. ");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("O CPF " + cpf + " é inválido. " + ex);
            }
        }
    }
}

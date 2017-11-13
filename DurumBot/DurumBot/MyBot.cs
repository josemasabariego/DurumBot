using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DurumBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;
        
        public MyBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '·';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            //Registro de comandos
            registrarComandoFotos();
            registrarComandoDado();
            registrarComandoManifiestate();
            registrarComandoEsGilipollas();

            commands.CreateCommand("hola")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("ARRODILLATE ANTE DURUM");
                });

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("Mzc5NjUxMTY1MzcxMjM2MzYy.DOtLIA._intQK27fMYbT5LeIDB05nVZV8E", TokenType.Bot);
            });
        }

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        #region Comandos
        private void registrarComandoFotos()
        {
            commands.CreateCommand("sendnudes")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile("imagenes/nude.jpg");
                });
        }

        private void registrarComandoDado()
        {
            commands.CreateCommand("dado")
                .Do(async (e) =>
                {
                    Random random = new Random();
                    await e.Channel.SendMessage("Ha salido un "+(random.Next(5)+1).ToString()+ " guau guau");
                });
        }

        private void registrarComandoManifiestate()
        {
            commands.CreateCommand("manifiestate")
                .Do(async (e) =>
                {              
                    await e.Channel.SendTTSMessage("Guau guau soy el dios Durum");
                });
        }

        private void registrarComandoEsGilipollas()
        {
            commands.CreateCommand("esgilipollas").Parameter("mensaje",ParameterType.Required)
                .Do(async (e) =>
                {
                    String str = e.Message.Text;
                    String[] tokens = str.Split(new[] { "·esgilipollas " }, StringSplitOptions.None);
                    String mensaje = tokens[1] + " es gilipollas";
                    await e.Channel.SendTTSMessage(mensaje);
                });
        }

        #endregion



    }
}

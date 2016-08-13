using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using CowsaySharp.GetCowsay.Containers;
using CowsaySharp.Library;
using CowsaySharp.Common;

//https://www.simple-talk.com/dotnet/net-development/using-c-to-create-powershell-cmdlets-the-basics/

namespace CowsaySharp.GetCowsay
{
    [Cmdlet(VerbsCommon.Get, "Cowsay")]
    [OutputType(typeof(Cowsay))]
    public class GetCowsayCmdlet : Cmdlet
    {
        [Parameter]
        [ValidateSet("borg","dead","greedy","paranoid","stoned","tired","wired","young")]
        [Alias("m")]
        public string mode { get; set; }

        [Parameter]
        [Alias("l")]
        public SwitchParameter list { get; set; }

        [Parameter]
        [Alias("n")]
        public SwitchParameter figlet { get; set; }

        [Parameter]
        [Alias("h")]
        public SwitchParameter help { get; set; }

        [Parameter]
        [Alias("e")]
        public string eyes { get; set; }

        [Parameter]
        [Alias("T")]
        public string tongue { get; set; }

        [Parameter]
        [Alias("f")]
        public string cowfile { get; set; }

        [Parameter]
        [Alias("W")]
        public int wrapcolumn { get; set; }

        [Parameter(ValueFromPipeline = true,Mandatory = true,ValueFromRemainingArguments = true)]
        public string message { get; set; }

        protected override void BeginProcessing()
        {
            //setup variables based on switches
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            //generate cow and assign to cowsay container to return
            //base.ProcessRecord();
            Console.WriteLine(BuildOutputObject().SpeechBubbleAndCow);
            WriteObject(BuildOutputObject());
           
        }

        private Cowsay BuildOutputObject()
        {
            return new Cowsay("cow", message);
            {

            };
        }
    }
}


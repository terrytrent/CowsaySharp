using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cowsay
{
    class cow
    {
        private string eyes;
        private string tongue;
        private string cowAscii;

        public string CowAscii
        {
            get { return cowAscii; }
            set { cowAscii = value; }
        }


        public string Eyes
        {
            get { return eyes; }
            set
            {
                if (value != null)
                {
                    int eyesLength = value.Length;
                    if (eyesLength != 2)
                    {
                        throw new ArgumentOutOfRangeException(nameof(Eyes), "Must be exactly 2 characters in length");
                    }
                }
                eyes = value;
            }
        }

        public string Tongue
        {
            get { return tongue; }
            set
            {
                if (value != null)
                {
                    int tongueLength = value.Length;
                    if (tongueLength > 2)
                    {
                        throw new ArgumentOutOfRangeException(nameof(Eyes), "Must be 1 or 2 characters in length");
                    }

                    if (tongueLength != 2)
                    {
                        tongue = $"a {value}";
                    }
                }
                tongue = value; }
        }

        public cow() : this(null, null)
        {

        }

        public cow(string cowEyes, string cowTongue)
        {
            
            

            if (Eyes == null)
                cowEyes = "oo";
            if (Tongue == null)
                cowTongue = "  ";
                
            Eyes = cowEyes;
            Tongue = cowTongue;

            CowAscii = $@"         \   ^__^ 
          \  ({Eyes})\_______
             (__)\       )\/\
              {Tongue} ||----w |
                 ||     ||";
        }

        public void showCow()
        {
            Console.WriteLine(CowAscii);
        }
    }
}

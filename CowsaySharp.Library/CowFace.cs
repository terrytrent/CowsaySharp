using System;

namespace CowsaySharp.Library
{
    public class CowFace
    {
        private string eyes;
        private string tongue;
        private bool ThreeEyes;

        public string Eyes
        {
            get
            {
                return eyes;
            }
            set
            {
                if(ThreeEyes)
                    eyes = value?.Substring(0, 3);
                else
                    eyes = value?.Substring(0, 2);
            }
        }

        public string Tongue
        {
            get
            {
                return tongue;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    tongue = "  ";
                else
                    tongue = value.Substring(0, 2);
            }
        }

        public CowFace() : this(null,null, false)
        {
        }

        public CowFace(string cowEyes) : this(cowEyes, null, false)
        {
        }

        public CowFace(string cowEyes, string cowTongue) : this(cowEyes,cowTongue,false)
        {
        }

        public CowFace(string cowEyes, string cowTongue, bool threeEyes)
        {
            Eyes = cowEyes;
            Tongue = cowTongue;
            ThreeEyes = threeEyes;
        }

    }
}

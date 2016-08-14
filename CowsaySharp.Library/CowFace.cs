using System;

namespace CowsaySharp.Library
{
    public class CowFace
    {
        public string Eyes { get; set; }
        public string Tongue { get; set; }

        public CowFace() { Eyes = null; Tongue = null; }

        public CowFace(string eyes) : this(eyes, null)
        {

        }

        public CowFace(string eyes, string tongue)
        {
            Eyes = eyes;
            if (String.IsNullOrEmpty(tongue))
                Tongue = "  ";
            else
                Tongue = tongue.Substring(0,1);
        }
    }
}

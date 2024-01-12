using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames
{
    public class ProgressBarAttribute : PropertyAttribute
    {
        public float maxValue;
        public string label;
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        
        public ProgressBarAttribute()
        {
            maxValue = 100;
            label = "";
            R = 028;
            G = 079;
            B = 128;
            A = 255;
        }
        
        public ProgressBarAttribute(float maxValue)
        {
            this.maxValue = maxValue;
            label = "";
            R = 028;
            G = 079;
            B = 128;
            A = 255;
        }
        
        public ProgressBarAttribute(string label)
        {
            maxValue = 100;
            this.label = label;
            R = 028;
            G = 079;
            B = 128;
            A = 255;
        }
        
        public ProgressBarAttribute(byte r, byte g, byte b, byte a = 255)
        {
            maxValue = 100;
            label = "";
            R = r;
            G = g;
            B = b;
            A = a;
        }
        
        public ProgressBarAttribute(float maxValue, string label)
        {
            this.maxValue = maxValue;
            this.label = label;
            R = 028;
            G = 079;
            B = 128;
            A = 255;
        }
        
        public ProgressBarAttribute(float maxValue, byte r, byte g, byte b, byte a = 255)
        {
            this.maxValue = maxValue;
            label = "";
            R = r;
            G = g;
            B = b;
            A = a;
        }
        
        public ProgressBarAttribute(string label, byte r, byte g, byte b, byte a = 255)
        {
            maxValue = 100;
            this.label = label;
            R = r;
            G = g;
            B = b;
            A = a;
        }
        
        public ProgressBarAttribute(float maxValue, string label, byte r, byte g, byte b, byte a = 255)
        {
            this.maxValue = maxValue;
            this.label = label;
            R = r;
            G = g;
            B = b;
            A = a;
        }
        
    } // class end
}

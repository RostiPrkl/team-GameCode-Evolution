using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames
{
    public class CustomCurveAttribute : PropertyAttribute
    {
        public bool hideLabel;
        public string customLabel;
        public byte R;
        public byte G;
        public byte B;
        public byte A;
 
        public CustomCurveAttribute()
        {
            hideLabel = false;
            customLabel = "";
            R = 050;
            G = 179;
            B = 000;
            A = 255;
        }
 
        public CustomCurveAttribute(bool hideLabel = false)
        {
            this.hideLabel = hideLabel;
            customLabel = "";
            R = 050;
            G = 179;
            B = 000;
            A = 255;
        }
 
        public CustomCurveAttribute(string customLabel)
        {
            hideLabel = false;
            this.customLabel = customLabel;
            R = 050;
            G = 179;
            B = 000;
            A = 255;
        }
 
        public CustomCurveAttribute(byte r = 000, byte g = 028, byte b = 045, byte a = 255)
        {
            hideLabel = false;
            customLabel = "";
            R = r;
            G = g;
            B = b;
            A = a;
        }
 
        public CustomCurveAttribute(bool hideLabel, byte r = 000, byte g = 028, byte b = 045, byte a = 255)
        {
            this.hideLabel = hideLabel;
            customLabel = "";
            R = r;
            G = g;
            B = b;
            A = a;
        }
 
        public CustomCurveAttribute(string customLabel, byte r = 000, byte g = 028, byte b = 045, byte a = 255)
        {
            hideLabel = false;
            this.customLabel = customLabel;
            R = r;
            G = g;
            B = b;
            A = a;
        }
        
    } // class end
}

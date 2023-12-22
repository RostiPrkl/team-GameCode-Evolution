using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames
{
    public class RangeLabelAttribute : PropertyAttribute
    {
        public float min;
        public float max;
        public string minLabel;
        public string maxLabel;

        public RangeLabelAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
            minLabel = MathUtility.RoundFloat(min, 3).ToString();
            maxLabel = MathUtility.RoundFloat(max, 3).ToString();
        }

        public RangeLabelAttribute(float min, float max, string minLabel, string maxLabel)
        {
            this.min = min;
            this.max = max;
            this.minLabel = minLabel;
            this.maxLabel = maxLabel;
        }
        
    } // class end
}

using System;
using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames
{
    [System.AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class InfoBoxAttribute : PropertyAttribute
    {
        public enum InfoBoxType
        {
            None,
            Info,
            Error,
            Warning
        }

        public string message;
        public InfoBoxType type;

        public InfoBoxAttribute(string message)
        {
            this.message = message;
            type = InfoBoxType.None;
        }

        public InfoBoxAttribute(string message, InfoBoxType type)
        {
            this.message = message;
            this.type = type;
        }

        /*
        public InfoBoxAttribute(string message, string condition)
        {
            this.message = message;
            type = InfoBoxType.None;
        }

        public InfoBoxAttribute(string message, InfoBoxType type, string condition)
        {
            this.message = message;
            this.type = type;
        }
        */
        
    } // class end
}

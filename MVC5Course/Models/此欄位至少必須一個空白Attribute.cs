using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
    internal class 此欄位至少必須一個空白Attribute : DataTypeAttribute
    {
        public 此欄位至少必須一個空白Attribute():base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            var str = (string)value;
            return str.Contains(" ");
        }
    }
}
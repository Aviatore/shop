// using System;  
// using System.Collections.Generic;  
// using System.ComponentModel.DataAnnotations;  
// using System.Linq;  
// using System.Web;  
//
// namespace shop.CustomValidation
// {
//     public class RequiredWhen : ValidationAttribute
//     {
//         private string otherPropName;
//         private bool otherPropValue;
//
//         public RequiredWhen(string name, bool value)
//         {
//             otherPropName = name;
//             otherPropValue = value;
//         }
//         // public override bool IsValid(object value)  
//         // {  
//         //     var otherPropId = $(element).data('val-other');
//         //     if (otherPropId) {
//         //         var otherProp = $(otherPropId)[0].checked;
//         //         if (otherProp) {
//         //             return element.value.length > 0;
//         //         }
//         //     }
//         //     return true;
//         // }  
//
//         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//         {
//             // TODO: validate the length of _others to ensure you have all required inputs
//             var property = validationContext.ObjectType.GetProperty(otherPropName);
//             if (property == null)
//             {
//                 return new ValidationResult(
//                     string.Format("Unknown property: {0}", otherPropName)
//                 );
//             }
//
//             // This is to get one of the other value information. 
//             var otherValue = property.GetValue(validationContext.ObjectInstance, null);
//             //
//             // if (otherValue == otherPropValue)
//             // {
//             //     
//             // }
//             // TODO: get the other value again for the date -- and then apply your business logic of determining the capacity          
//         }
//     }
// }
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MohDemo.Utility.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MohDemo.Utility.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to همه فرمهای &quot; {0} &quot; به نقش &quot; {1} &quot; با موفقیت انتصاب یافت..
        /// </summary>
        public static string AssignAllForm {
            get {
                return ResourceManager.GetString("AssignAllForm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to فرم &quot; {0} &quot; به نقش &quot; {1} &quot; با موفقیت انتصاب یافت..
        /// </summary>
        public static string AssignFormToRole {
            get {
                return ResourceManager.GetString("AssignFormToRole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to دسترسی همه فرمهای &quot; {0} &quot; از نقش &quot; {1} &quot; گرفته شد..
        /// </summary>
        public static string DeleteAllForm {
            get {
                return ResourceManager.GetString("DeleteAllForm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to دسترسی فرم &quot; {0} &quot; از نقش &quot; {1} &quot; گرفته شد..
        /// </summary>
        public static string DeleteFormFromRole {
            get {
                return ResourceManager.GetString("DeleteFormFromRole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مقدار {0} تکراری می باشد..
        /// </summary>
        public static string DuplicateError {
            get {
                return ResourceManager.GetString("DuplicateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ایمیل وارد شده نا معتبر می باشد.
        /// </summary>
        public static string EmailError {
            get {
                return ResourceManager.GetString("EmailError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to سوابق خطای رخ داده درون برنامه ذخیره شده و کارشناسان شرکت به زودی آنرا بر طرف خواهند کرد..
        /// </summary>
        public static string ErrorMessage {
            get {
                return ResourceManager.GetString("ErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to طول مقدار {0} باید حداقل دارای {2} کاراکتر باشد..
        /// </summary>
        public static string LengthError {
            get {
                return ResourceManager.GetString("LengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مقدار {0} نا معتبر می باشد..
        /// </summary>
        public static string NationalCodeError {
            get {
                return ResourceManager.GetString("NationalCodeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مقدار {0} بایستی عدد باشد..
        /// </summary>
        public static string NumberError {
            get {
                return ResourceManager.GetString("NumberError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ورود {0} الزامی است..
        /// </summary>
        public static string RequierdError {
            get {
                return ResourceManager.GetString("RequierdError", resourceCulture);
            }
        }
    }
}

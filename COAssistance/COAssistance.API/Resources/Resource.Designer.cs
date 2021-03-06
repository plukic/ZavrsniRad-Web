﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace COAssistance.API.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("COAssistance.API.Resources.Resource", typeof(Resource).Assembly);
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
        ///   Looks up a localized string similar to Pristupni podaci.
        /// </summary>
        public static string AccountLoginDataEmailSubject {
            get {
                return ResourceManager.GetString("AccountLoginDataEmailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Status za promjenu nije validan..
        /// </summary>
        public static string InvalidStatusChangeParameter {
            get {
                return ResourceManager.GetString("InvalidStatusChangeParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nova lozinka.
        /// </summary>
        public static string NewPassword {
            get {
                return ResourceManager.GetString("NewPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tajni kod.
        /// </summary>
        public static string PasswordResetTokenEmailSubject {
            get {
                return ResourceManager.GetString("PasswordResetTokenEmailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tajni kod nije ispravan.
        /// </summary>
        public static string PasswordResetTokenNotValid {
            get {
                return ResourceManager.GetString("PasswordResetTokenNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Korisnički nalog već postoji..
        /// </summary>
        public static string UserAccountExists {
            get {
                return ResourceManager.GetString("UserAccountExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vaša pretplata je istekla..
        /// </summary>
        public static string UserAccountHasExpired {
            get {
                return ResourceManager.GetString("UserAccountHasExpired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Korisnički nalog nije aktivan..
        /// </summary>
        public static string UserAccountNotActive {
            get {
                return ResourceManager.GetString("UserAccountNotActive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Korisnik nema unesen email..
        /// </summary>
        public static string UserHasNotEnteredEmail {
            get {
                return ResourceManager.GetString("UserHasNotEnteredEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Korisničko ime ili lozinka nisu ispravni..
        /// </summary>
        public static string UsernameOrPasswordNotValid {
            get {
                return ResourceManager.GetString("UsernameOrPasswordNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pristupni podaci korisničkog naloga.
        /// </summary>
        public static string UsersProfileData {
            get {
                return ResourceManager.GetString("UsersProfileData", resourceCulture);
            }
        }
    }
}

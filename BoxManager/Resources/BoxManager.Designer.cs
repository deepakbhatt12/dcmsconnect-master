﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DcmsMobile.BoxManager.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class BoxManager {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BoxManager() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DcmsMobile.BoxManager.Resources.BoxManager", typeof(BoxManager).Assembly);
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
        ///   Looks up a localized string similar to {0} is too long. Should be less than {1} characters..
        /// </summary>
        public static string MaxStringLengthErrorMessage {
            get {
                return ResourceManager.GetString("MaxStringLengthErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #Cartons.
        /// </summary>
        public static string Name_CartonCount {
            get {
                return ResourceManager.GetString("Name_CartonCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected Cartons.
        /// </summary>
        public static string Name_ExpectedCartons {
            get {
                return ResourceManager.GetString("Name_ExpectedCartons", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to # Pallets.
        /// </summary>
        public static string Name_PalletCount {
            get {
                return ResourceManager.GetString("Name_PalletCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Process.
        /// </summary>
        public static string Name_Process {
            get {
                return ResourceManager.GetString("Name_Process", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be between {1} and {2} characters.
        /// </summary>
        public static string RangeMinMaxErrorMessage {
            get {
                return ResourceManager.GetString("RangeMinMaxErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is required.
        /// </summary>
        public static string RequiredErrorMessage {
            get {
                return ResourceManager.GetString("RequiredErrorMessage", resourceCulture);
            }
        }
    }
}

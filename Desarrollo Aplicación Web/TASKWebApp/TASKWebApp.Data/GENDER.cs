//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TASKWebApp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class GENDER
    {
        public GENDER()
        {
            this.USER_INFO = new HashSet<USER_INFO>();
        }
    
        public decimal ID { get; set; }
        public string NAME { get; set; }
    
        public virtual ICollection<USER_INFO> USER_INFO { get; set; }
    }
}

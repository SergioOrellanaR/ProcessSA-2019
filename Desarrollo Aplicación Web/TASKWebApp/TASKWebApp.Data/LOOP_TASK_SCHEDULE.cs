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
    
    public partial class LOOP_TASK_SCHEDULE
    {
        public LOOP_TASK_SCHEDULE()
        {
            this.PROCESSED_TASK = new HashSet<PROCESSED_TASK>();
        }
    
        public decimal ID { get; set; }
        public decimal ID_LOOP_TASK { get; set; }
        public Nullable<decimal> ID_DAYOFWEEK { get; set; }
        public Nullable<decimal> DAYOFMONTH { get; set; }
        public Nullable<decimal> NUMBEROFWEEK { get; set; }
        public Nullable<decimal> ID_MONTH { get; set; }
    
        public virtual DAY_OF_WEEK DAY_OF_WEEK { get; set; }
        public virtual LOOP_TASK LOOP_TASK { get; set; }
        public virtual MONTH MONTH { get; set; }
        public virtual ICollection<PROCESSED_TASK> PROCESSED_TASK { get; set; }
    }
}

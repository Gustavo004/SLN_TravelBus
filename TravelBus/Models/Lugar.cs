//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelBus.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lugar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lugar()
        {
            this.Viaje = new HashSet<Viaje>();
            this.Viaje1 = new HashSet<Viaje>();
        }
    
        public int IIDLUGAR { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Viaje> Viaje { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Viaje> Viaje1 { get; set; }
    }
}

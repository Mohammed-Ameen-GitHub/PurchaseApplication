//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PurchaseApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SalesOrderLine
    {
        public int Id { get; set; }
        public string ItemNo { get; set; }
        public string QuantityOrdered { get; set; }
        public string Price { get; set; }
        public string LineTotal { get; set; }

        public virtual SalesOrderHeader SalesOrderHeader { get; set; }
    }
}
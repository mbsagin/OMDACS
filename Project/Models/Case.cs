//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;

    public partial class Case
    {
        public int CaseID { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> SymptomsBegan { get; set; }
        public string CurrentMedications { get; set; }
        public string BodySystemsAffected { get; set; }
        public string SymptomsDetail { get; set; }
        public string MedicalHistory { get; set; }
        public string PersonalStruggle { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public string User { get; set; }
    }
}

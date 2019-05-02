using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
    /*Del av maskininlärning, lagrar hur många gånger man navigerat 
    från bransch A till bransch B efter en utförd bokning.*/
	public class BranchRelation {
        [Key]
        
		public virtual int BranchRelationId { get; set; }
		public virtual int CountClick { get; set; }


        public virtual string branchBId2 { get; set; }


        //[ForeignKey("BranchB")]
        //public int? BranchBId { get; set; }
        //                                   //     public Branch Branch { get; set; }
        //public virtual Branch BranchB { get; set; }

    }
}
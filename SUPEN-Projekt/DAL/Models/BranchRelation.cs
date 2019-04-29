using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {
    /*Del av maskininlärning, lagrar hur många gånger man navigerat 
    från bransch A till bransch B efter en utförd bokning.*/
	public class BranchRelation {
		public virtual int BranchRelationId { get; set; }
		public virtual int CountClick { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
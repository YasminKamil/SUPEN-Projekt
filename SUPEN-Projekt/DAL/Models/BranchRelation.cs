using System.ComponentModel.DataAnnotations;

namespace SUPEN_Projekt.Models {
	/*Del av maskininlärning, lagrar hur många gånger man navigerat 
    från bransch A till bransch B efter en utförd bokning.*/
	public class BranchRelation {
		[Key]
		public virtual int BranchRelationId { get; set; }
		public virtual int CountClick { get; set; }
		public virtual string branchBId2 { get; set; }
	}
}
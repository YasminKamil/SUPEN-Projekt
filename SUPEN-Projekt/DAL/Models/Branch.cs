using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUPEN_Projekt.Models {

	/*En modell för brancher med sina egenskaper, som representerar en tabell i databasen. 
	 Med ett till många samband till BranchReations*/
	public class Branch {

        [Key]
		public virtual int BranchId { get; set; }
		public virtual string BranchName { get; set; }

        public virtual string PictureUrl { get; set; }
        public virtual ICollection<BranchRelation> BranchRelations { get; set; }
    }
}
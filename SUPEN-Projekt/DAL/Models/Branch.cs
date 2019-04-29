using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Models {

	public class Branch {
        [Key]
		public virtual int BranchId { get; set; }
		public virtual string BranchName { get; set; }

        public virtual ICollection<BranchRelation> BranchRelations { get; set; }
    }
}
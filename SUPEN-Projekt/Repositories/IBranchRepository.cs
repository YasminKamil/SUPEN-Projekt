using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Repositories {
	public interface IBranchRepository : IRepository<Branch> {
		IEnumerable<Branch> GetAllBranches();
	}
}
using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Repositories {
	public class BranchRepository : Repository<Branch>, IBranchRepository{

		public BranchRepository(ApplicationDbContext context) : base(context) {
		}

		public IEnumerable<Branch> GetAllBranches() {
			return ApplicationDbContext.Set<Branch>().ToList();
		}

		public ApplicationDbContext ApplicationDbContext {
			get {
				return Context as ApplicationDbContext;
			}
		}
	}
}
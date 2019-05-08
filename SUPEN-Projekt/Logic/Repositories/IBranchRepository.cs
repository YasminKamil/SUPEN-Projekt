using SUPEN_Projekt.Models;
using System.Collections.Generic;

namespace SUPEN_Projekt.Repositories {
	//Interface för BranchRepository för att kunna återanvända metoderna i presentationslagret/API:er
	public interface IBranchRepository : IRepository<Branch> {
		void CreateBranchRelation(int branchA, int branchB);
		void AddClickToBranchRelation(int branchA, int branchB);
		BranchRelation GetBranchRelation(int branchA, int branchB);
		IEnumerable<BranchRelation> GetBranchRelations(int branchA);
	}

}
